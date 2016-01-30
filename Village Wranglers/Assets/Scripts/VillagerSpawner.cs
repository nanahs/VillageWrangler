using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillagerSpawner : MonoBehaviour {

	public GameObject VillagerPrefab;

	List<Transform> huts;
	public int totalActiveVillagersAllowed = 5;
	int numActiveVillagers = 0;


	// Use this for initialization
	void Start () {

		huts = new List<Transform>();

		for(int i = 0; i < this.transform.childCount; i++){
			huts.Add(this.transform.GetChild(i).transform);
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if(numActiveVillagers <  totalActiveVillagersAllowed){
			spawnVillager();
			numActiveVillagers++;
		}
	
	}

	private void spawnVillager(){
		
		//Pick a random hut
		Vector3 villagerSpawnLoc = huts[Random.Range(0, huts.Count)].position;
		villagerSpawnLoc.y = 1;
		Instantiate(VillagerPrefab, villagerSpawnLoc, Quaternion.identity);
	}
}
