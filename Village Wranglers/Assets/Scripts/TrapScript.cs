using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

	powerupTrapSpawner putSpawn;

	// Use this for initialization
	void Start () {
		putSpawn = GetComponentInParent<powerupTrapSpawner>();
	}

	void OnDestroy(){

		if(putSpawn){
			putSpawn.pickUpTaken();
		}

	}
}
