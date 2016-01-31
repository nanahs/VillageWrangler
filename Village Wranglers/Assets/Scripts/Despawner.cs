using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Villager"){
			Destroy(col.gameObject);
		}
		if(col.collider.tag == "Player"){

			//Respawn player at his base, taken care of in player script
			col.gameObject.SetActive(false);

		}
	}
}
