using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 5f;
	public float reducedThrowSpeed = .5f;
	public float throwUpSpeed = 1f;
	public Transform holdLocation;
	GameObject heldVillager;
	Rigidbody heldVillagerRigid;
	GameObject heldVillagerStrength;
	Rigidbody rb;

	float xTemp, yTemp;
	float xMov, yMov;
	bool isHoldingVillager = false;

	public float pickupCD = .5f;
	private bool canAction = true;

	public float TESTVAR;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody>();
		//Debug.Log(Input.GetJoystickNames()[0]);
	
	}
	
	// Update is called once per frame
	void Update () {

		/*
		for(int i = 1; i < 12;i++){
			if(Input.GetKeyDown("joystick "+ i + " button 18")){
				Debug.Log("Joystick is #" + i);
			}
		}
		*/
		

		xTemp = Input.GetAxis("P1_Horizontal");
		yTemp = Input.GetAxis("P1_Vertical");
		float xMov = 0f; 
		float yMov = 0f;

		if(xTemp > .1 || xTemp < .1){
			xMov = xTemp * Time.deltaTime * moveSpeed;
		}

		if(yMov > .1 || yMov < .1){
			yMov = yTemp * Time.deltaTime * moveSpeed;
		}

		//cc.Move(new Vector3(xMov, 0, -yMov));
		rb.velocity = new Vector3(xMov, 0, -yMov);
		//rb.MovePosition(new Vector3(xMov, 0, -yMov));

		if(isHoldingVillager && canAction){
			if(Input.GetButtonDown("P1_Pickup")){

				throwVillager(new Vector3(xMov, throwUpSpeed, -yMov));

			}
		}
	}

	void OnTriggerStay(Collider other){

		if(other.tag == "Villager" && isHoldingVillager == false && canAction){

			if(Input.GetButtonDown("P1_Pickup")){

				Debug.Log("Pickup villager");

				heldVillager = other.gameObject;
				heldVillager.transform.position = holdLocation.position;
				heldVillager.transform.parent = holdLocation;
				heldVillagerRigid = heldVillager.GetComponent<Rigidbody>();
				heldVillagerRigid.isKinematic = true;
				//heldVillagerRigid.useGravity = false;
				isHoldingVillager = true;
				canAction = false;

				startCooldown();
			}

		}



	}

	private void throwVillager(Vector3 throwAngle){


		isHoldingVillager = false;
		heldVillagerRigid.isKinematic = false;
		//Vector3 throwAngle = new Vector3(xMov * reducedThrowSpeed, 0, -yMov * reducedThrowSpeed);
		heldVillager.transform.parent = null;
		heldVillagerRigid.velocity = throwAngle * reducedThrowSpeed;

		Debug.Log(throwAngle);
		Debug.Log(heldVillagerRigid.velocity);

		startCooldown();

	}

	private void startCooldown(){
		canAction = false;
		StartCoroutine("pickupCooldown");

	}

	IEnumerator pickupCooldown(){

		TESTVAR = pickupCD;
		while(TESTVAR > 0){
			TESTVAR = TESTVAR - 1 * Time.deltaTime;
			yield return null;
		}

		canAction = true;

	}
}
