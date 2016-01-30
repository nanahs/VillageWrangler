using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {

	public GameObject Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// rudimentary movement for a pill to be replaced with joystick input later.
		if (Input.GetKey(KeyCode.W))
			Player.transform.Translate(new Vector3(0.0f, 0.0f, 0.5f));
		if (Input.GetKey(KeyCode.A))
			Player.transform.Translate(new Vector3(-0.5f, 0.0f, 0.0f));
		if (Input.GetKey(KeyCode.D))
			Player.transform.Translate(new Vector3(0.5f, 0.0f, 0.0f));
		if (Input.GetKey(KeyCode.S))
			Player.transform.Translate(new Vector3(0.0f, 0.0f, -0.5f));
	}

	void PushAction()
	{

	}

	void PickupAction()
	{

	}
}
