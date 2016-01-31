using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioArrays))]
public class AudioController : MonoBehaviour {

	AudioArrays audioFiles;

	// Use this for initialization
	void Start () {
		audioFiles = GetComponent<AudioArrays>();
		audioFiles.playGameMusic();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
