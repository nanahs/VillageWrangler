using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 5f;
	public float speedBoost = 2f;
	public float speedBoostTime = 5f;
	public float strengthBoostTime = 5f;
	public float dizzyCurseTime = 5f;
	public float reducedThrowSpeed = .5f;
	public float throwUpSpeed = 1f;
	public float pickupCD = .5f;
	public float visibleCDcounter;
	public float trapLifeTime = 2f;

	public Transform holdLocation;
	public bool isPlayerOne = true;
	public bool isStrong = false;
	public bool isDizzy = false;


	GameObject heldVillager;
	VillagerController heldVillagerScript;
	Rigidbody heldVillagerRigid;
	Rigidbody rb;

	float xTemp, yTemp;
	float xMov, yMov;

	bool isHoldingVillager = false;
	bool isWalking = false;
	bool hasThrown = false;

	private bool canAction = true;

	private string playerOne = "P1_";
	private string playerTwo = "P2_";
	private string horizontal = "Horizontal";
	private string vertical = "Vertical";

	private string randomHorz = "";
	private string randomVert = "";
	private int randomDirection = 1;

	Animator anim;

	bool isFacingRight = true;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(isHoldingVillager && holdLocation.childCount == 0 ){
			heldVillager = null;
			heldVillagerRigid = null;
			heldVillagerScript = null;
		}

		//checkJoystickInput();

		/*
		if(Input.GetKeyDown(KeyCode.Space)){
			this.gameObject.SetActive(false);
		}
		*/

		checkDirection();

		if(xMov != 0 || yMov != 0){
			isWalking = true;
		}else{
			isWalking = false;
		}

		anim.SetBool("isHolding", isHoldingVillager);
		anim.SetBool("isWalking", isWalking);



		xMov = 0f; 
		yMov = 0f;
		xTemp = 0f;
		yTemp = 0f;

		if(!isDizzy){

			if(isPlayerOne){
				xTemp = Input.GetAxis("P1_Horizontal");
				yTemp = Input.GetAxis("P1_Vertical");
			}else{
				xTemp = Input.GetAxis("P2_Horizontal");
				yTemp = Input.GetAxis("P2_Vertical");
			}

		}else{

			if(isPlayerOne){
				xTemp = Input.GetAxis("P1_Horizontal") * randomDirection;
				yTemp = Input.GetAxis("P1_Vertical") * randomDirection;
			}else{
				xTemp = Input.GetAxis("P2_Horizontal") * randomDirection;
				yTemp = Input.GetAxis("P2_Vertical") * randomDirection;
			}

		}



		if(xTemp > .5 || xTemp < .5){
			xMov = xTemp;
		}else{
			xMov = 0;
		}

        
		if(yMov > .5 || yMov < .5){
			yMov = yTemp ;
		}else{
			yMov = 0;
		}


		rb.velocity = new Vector3(xMov, 0, -yMov).normalized * Time.deltaTime * moveSpeed;
		//Debug.Log(rb.velocity);

		if(isHoldingVillager && canAction){

			if(isPlayerOne){

				if(Input.GetButtonDown("P1_Pickup")){

					throwVillager(new Vector3(rb.velocity.x, throwUpSpeed, rb.velocity.z));

				}
			}
			else{
				
				if(Input.GetButtonDown("P2_Pickup")){

					throwVillager(new Vector3(rb.velocity.x, throwUpSpeed, rb.velocity.z));

				}
			}






		}
	}

	void OnTriggerEnter(Collider other){


		//Powerups
		if(other.tag == "speed"){

			Destroy(other.gameObject);

			increaseSpeed();
			Invoke("reduceSpeed", speedBoostTime);
		}

		if(other.tag == "strength"){

			Destroy(other.gameObject);

			isStrong = true;
			Invoke("reduceStrength", strengthBoostTime);
		}

		if(other.tag == "dizzy"){

			Destroy(other.gameObject);

			isDizzy = true;
			randomMovementControls();

			Invoke("fixMovementControls", dizzyCurseTime);
		}


		//Traps
		if(other.tag == "tarPit"){

			Destroy(other.gameObject);
			Debug.Log("slow");


			reduceSpeed();
			Invoke("increaseSpeed", trapLifeTime);
		}


	}

	void OnTriggerStay(Collider other){

		if(other.tag == "Villager" && isHoldingVillager == false && canAction){

			if(isPlayerOne){

				if(Input.GetButtonDown("P1_Pickup")){

					isHoldingVillager = true;

					//Set held villager components for later use
					heldVillager = other.gameObject;
					heldVillagerScript = heldVillager.GetComponent<VillagerController>();
					heldVillagerRigid = heldVillager.GetComponent<Rigidbody>();

					//Put villager above player and disable his movement
					heldVillager.transform.position = holdLocation.position;
					heldVillager.transform.parent = holdLocation;
					heldVillagerRigid.isKinematic = true;
					heldVillagerScript.setHoldState(isHoldingVillager);

					canAction = false;
					startCooldown();
				}

			}else{

				if(Input.GetButtonDown("P2_Pickup")){

					isHoldingVillager = true;

					//Set held villager components for later use
					heldVillager = other.gameObject;
					heldVillagerScript = heldVillager.GetComponent<VillagerController>();
					heldVillagerRigid = heldVillager.GetComponent<Rigidbody>();

					//Put villager above player and disable his movement
					heldVillager.transform.position = holdLocation.position;
					heldVillager.transform.parent = holdLocation;
					heldVillagerRigid.isKinematic = true;
					heldVillagerScript.setHoldState(isHoldingVillager);

					canAction = false;
					startCooldown();
				}
				
			}



		}



	}

	private void throwVillager(Vector3 throwAngle){

		anim.Play("Chief_Throw");

		isHoldingVillager = false;
		heldVillagerRigid.isKinematic = false;
		heldVillager.transform.parent = null;
		heldVillagerRigid.velocity = throwAngle * reducedThrowSpeed;
		heldVillagerScript.thrown();
		//heldVillagerScript.setHoldState(isHoldingVillager);

		heldVillager = null;
		heldVillagerRigid = null;
		heldVillagerScript = null;

		//Debug.Log(throwAngle);
		//Debug.Log(heldVillagerRigid.velocity);

		startCooldown();

	}

	private void startCooldown(){
		canAction = false;
		StartCoroutine("pickupCooldown");

	}

	IEnumerator pickupCooldown(){

		visibleCDcounter = pickupCD;
		while(visibleCDcounter > 0){
			visibleCDcounter = visibleCDcounter - 1 * Time.deltaTime;
			yield return null;
		}

		canAction = true;

	}

	private void respawnPlayer(){

		rb.velocity = Vector3.zero;

		if(isPlayerOne){
			this.transform.position = GameObject.FindGameObjectWithTag("playerOneSpawn").transform.position;
		}else{
			this.transform.position = GameObject.FindGameObjectWithTag("playerTwoSpawn").transform.position;
		}

		this.gameObject.SetActive(true);
		
	}

	private void reduceSpeed(){
		moveSpeed /= speedBoost;
	}
	private void increaseSpeed(){
		moveSpeed *= speedBoost;
	}
	private void reduceStrength(){
		isStrong = false;
	}
	private void randomMovementControls(){

		List<string> movements = new List<string>();

		if(isPlayerOne){
			randomHorz = playerOne;
			randomVert = playerOne;
		}else{
			randomHorz = playerTwo;
			randomVert = playerTwo;
		}

		movements.Add(horizontal);
		movements.Add(vertical);

		int temp = Random.Range(0, 2);

		randomHorz = movements[temp];
		randomVert = movements[0 - temp + 1];
		randomDirection = Random.Range(-1, 2);

	}
	private void fixMovementControls(){
		isDizzy = false;
	}

	public void OnDisable(){

		Invoke("respawnPlayer", 2f);

	}

	void checkJoystickInput(){


		if(Input.GetKeyDown("joystick 1 button 16")){
			Debug.Log("joystick 1");
		}

		if(Input.GetKeyDown("joystick 2 button 16")){
			Debug.Log("joystick 2");
		}

		if(Input.GetKeyDown("joystick 3 button 16")){
			Debug.Log("joystick 3");
		}

		if(Input.GetKeyDown("joystick 4 button 16")){
			Debug.Log("joystick 4");
		}

		if(Input.GetKeyDown("joystick 5 button 16")){
			Debug.Log("joystick 5");
		}

		if(Input.GetKeyDown("joystick 6 button 16")){
			Debug.Log("joystick 6");
		}

		if(Input.GetKeyDown("joystick 7 button 16")){
			Debug.Log("joystick 7");
		}

		if(Input.GetKeyDown("joystick 8 button 16")){
			Debug.Log("joystick 8");
		}

		if(Input.GetKeyDown("joystick 9 button 16")){
			Debug.Log("joystick 9");
		}

		if(Input.GetKeyDown("joystick 10 button 16")){
			Debug.Log("joystick 10");
		}

		if(Input.GetKeyDown("joystick 11 button 16")){
			Debug.Log("joystick 11");
		}


	}

	void checkDirection(){
		if(rb.velocity.x >= 0 && !isFacingRight){
			FlipSpriteX();
		}else if(rb.velocity.x < 0 && isFacingRight){
			FlipSpriteX();
		}
	}

	void FlipSpriteX(){
		isFacingRight = !isFacingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;

		transform.localScale = scale;
	}

}
