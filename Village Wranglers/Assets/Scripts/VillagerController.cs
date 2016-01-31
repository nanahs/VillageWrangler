using UnityEngine;
using System.Collections;

public class VillagerController : MonoBehaviour {

	public float walkSpeed = 5f;
	Vector3 walkDirection;

	Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
		InvokeRepeating("changeDirection", 0f, 5f);
	
	}
	
	// Update is called once per frame
	void Update () {

		rb.velocity = walkDirection*Time.deltaTime;
	}

	private void changeDirection(){
		
		walkDirection = Random.insideUnitSphere * 10 * walkSpeed;
		walkDirection.y = 0;

	}
}
