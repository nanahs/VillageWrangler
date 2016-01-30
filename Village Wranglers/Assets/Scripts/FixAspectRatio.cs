using UnityEngine;
using System.Collections;

public class FixAspectRatio : MonoBehaviour {

	public float targetRatio = 9f/16f;

	// Use this for initialization
	void Start () {
		Camera cam  = GetComponent<Camera>();
		cam.aspect = targetRatio;
	}
}
