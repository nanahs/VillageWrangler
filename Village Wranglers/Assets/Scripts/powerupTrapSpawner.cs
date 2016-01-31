using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class powerupTrapSpawner : MonoBehaviour {

	public GameObject speedBoost;
	public GameObject dizzy;
	public GameObject tarpit;
	public GameObject bearTrap;

	public int maxNumPickUps = 2;
	public int currNumPickups = 0;
	public float powerUpWeight = .7f;
	public float speedWeight = .7f;
	public float tarPitWeight = .7f;

	public float spawnStartTimer = 7f;
	public float spawnEndTimer = 15f;

	List<Transform> powerupLocs;

	// Use this for initialization
	void Start () {

		powerupLocs = new List<Transform>();

		for(int i = 0; i < this.transform.childCount; i++){
			powerupLocs.Add(this.transform.GetChild(i).transform);
		}

		Invoke("trySpawning", Random.Range(spawnStartTimer, spawnEndTimer));
	
	}

	private void trySpawning(){
		if(currNumPickups <  maxNumPickUps){
			spawnPickUp();
			currNumPickups++;
		}

		Invoke("trySpawning", Random.Range(spawnStartTimer, spawnEndTimer));
	}

	private void spawnPickUp(){

		if(Random.Range(0f, 1f) <= powerUpWeight){

			if(Random.Range(0f, 1f) <= speedWeight){
				spawnSpeed();
			}else{
				//spawnDizzy();
				spawnSpeed();
			}

		}else{

			if(Random.Range(0f, 1f) <= tarPitWeight){
				spawnTarPit();
			}else{
				spawnBearTrap();
			}
		}
	}

	private void spawnSpeed(){
		Transform spawnLoc = getSpawnLoc();
		GameObject temp = Instantiate(speedBoost, spawnLoc.position, Quaternion.identity) as GameObject;
		temp.transform.parent = spawnLoc;
	}
	private void spawnDizzy(){
		Transform spawnLoc = getSpawnLoc();
		GameObject temp = Instantiate(dizzy, spawnLoc.position, Quaternion.identity) as GameObject;
		temp.transform.parent = spawnLoc;
	}
	private void spawnTarPit(){
		Transform spawnLoc = getSpawnLoc();
		GameObject temp = Instantiate(tarpit, spawnLoc.position, Quaternion.identity) as GameObject;
		temp.transform.parent = spawnLoc;
	}
	private void spawnBearTrap(){
		Transform spawnLoc = getSpawnLoc();
		GameObject temp = Instantiate(bearTrap, spawnLoc.position, Quaternion.identity) as GameObject;
		temp.transform.parent = spawnLoc;
	}

	private Transform getSpawnLoc(){

		//Pick a random pickup Location
		Transform chosenLoc = powerupLocs[Random.Range(0, powerupLocs.Count)];

		while(chosenLoc.childCount > 0){
			chosenLoc = powerupLocs[Random.Range(0, powerupLocs.Count)];
		}

		Debug.Log(chosenLoc.position);
		return chosenLoc;

	}

	public void pickUpTaken(){
		currNumPickups--;
	}
}
