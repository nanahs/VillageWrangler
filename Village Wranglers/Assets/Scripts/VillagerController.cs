using UnityEngine;
using System.Collections;

public class VillagerController : MonoBehaviour {

	public float walkSpeed = 5f;
	Vector3 walkDirection;

	Rigidbody rb;

	public bool isHeld = false;
	public bool canMove = true;
	public bool isCaged = false;

	GameObject villageSpawner;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
		villageSpawner = GameObject.FindGameObjectWithTag("VillageSpawner");
		//InvokeRepeating("changeDirection", 0f, 5f);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(canMove){
			rb.velocity = walkDirection*Time.deltaTime;
		}
	}

	private void changeDirection(){


		if(!canMove){
			return;
		}


		if(isHeld){
			CancelInvoke("changeDirection");
			Debug.Log("stopped changing direction");
		}else{

			float temp = rb.velocity.y;

			walkDirection = Random.insideUnitSphere.normalized * walkSpeed;
			walkDirection.y = temp;
		}

	}

	private void cagedChangeDirection(){
		float temp = rb.velocity.y;

		walkDirection = Random.insideUnitSphere.normalized * walkSpeed;
		walkDirection.y = temp;
	}

	private void hitGround(){
		
		InvokeRepeating("changeDirection", 3f, 5f);
		canMove = true;

	}

	public void setHoldState(bool held){
		
		isHeld = held;
		if(isHeld){
			canMove = false;
		}

	}

	void OnCollisionEnter(Collision col){

		if(col.collider.tag == "Ground"){
			hitGround();
		}

	}

	void OnDestroy(){

		if(villageSpawner){
			villageSpawner.GetComponent<VillagerSpawner>().villagerDied();
		}

	}
}
