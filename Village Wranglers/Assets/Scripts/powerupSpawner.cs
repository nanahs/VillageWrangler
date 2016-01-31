using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class powerupSpawner : MonoBehaviour {

	public GameObject speedBoost;
	public GameObject strength;
	public GameObject dizzy;

	public int maxNumPowerups = 2;
	public int currNumPowerups = 0;
	public float speedSpawnChance = .7f;

	List<Transform> powerupLocs;

	// Use this for initialization
	void Start () {

		powerupLocs = new List<Transform>();

	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	private void spawnPowerup(){

		float item = Random.Range(0, speedSpawnChance);
		if(Random.Range(0, speedSpawnChance) <= .7){
			
		}

	}
}
