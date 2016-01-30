using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 5f;
	public float reducedThrowSpeed = .5f;
	public Transform holdLocation;
	GameObject heldVillager;
	Rigidbody heldVillagerRigid;
	GameObject heldVillagerStrength;
	Rigidbody rb;

	float xTemp, yTemp;
	float xMov, yMov;
	bool isHoldingVillager = false;


	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody>();
		//Debug.Log(Input.GetJoystickNames()[0]);
	
	}
	
	// Update is called once per frame
	void Update () {
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
	
		if(isHoldingVillager){
			if(Input.GetButtonDown("P1_Pickup")){

				throwVillager();

			}
		}
	}

	void OnTriggerStay(Collider other){

		if(other.tag == "Villager" && isHoldingVillager == false){

			if(Input.GetButtonDown("P1_Pickup")){

				Debug.Log("Pickup villager");

				heldVillager = other.gameObject;
				heldVillager.transform.position = holdLocation.position;
				heldVillager.transform.parent = holdLocation;
				heldVillagerRigid = heldVillager.GetComponent<Rigidbody>();
				heldVillagerRigid.useGravity = false;
				isHoldingVillager = true;
			}

		}



	}

	private void throwVillager(){

		isHoldingVillager = false;
		Vector3 throwAngle = new Vector3(xMov * reducedThrowSpeed, 0, -yMov * reducedThrowSpeed);
		heldVillagerRigid.useGravity = true;
		heldVillager.transform.parent = null;
		heldVillagerRigid.velocity = throwAngle;

	}
}
