using UnityEngine;
using System.Collections;

public class timer : MonoBehaviour {

	float counter;
	// Use this for initialization
	void Start()
	{
		counter = 0f;
	}

	// Update is called once per frame
	void Update()
	{
		counter += Time.deltaTime;
		if (counter >= 10)
		{
			Application.LoadLevel(4);
		}
	}
}
