using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	public float rotationSpeed = 200f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
	
	}
}
