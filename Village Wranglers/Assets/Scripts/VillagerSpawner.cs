using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillagerSpawner : MonoBehaviour {

	public GameObject VillagerPrefab;
	public float spawnStartTimer = 1f;
	public float spawnEndTimer = 10f;

	List<Transform> huts;
	public int totalActiveVillagersAllowed = 5;
	public int numActiveVillagers = 0;


	// Use this for initialization
	void Start () {

		huts = new List<Transform>();

		for(int i = 0; i < this.transform.childCount; i++){
			huts.Add(this.transform.GetChild(i).transform);
		}

		Invoke("trySpawning", Random.Range(spawnStartTimer, spawnEndTimer));

	}
	
	// Update is called once per frame
	void Update () {


	
	}

	private void trySpawning(){
		if(numActiveVillagers <  totalActiveVillagersAllowed){
			spawnVillager();
			numActiveVillagers++;
		}

		Invoke("trySpawning", Random.Range(spawnStartTimer, spawnEndTimer));
	}

	private void spawnVillager(){
		
		//Pick a random hut
		Vector3 villagerSpawnLoc = huts[Random.Range(0, huts.Count)].position;
		villagerSpawnLoc.y = 1;
		Instantiate(VillagerPrefab, villagerSpawnLoc, Quaternion.identity);
	}

	public void villagerDied(){
		numActiveVillagers--;
	}

	public void addActiveVillager()
	{
		numActiveVillagers++;
	}
}
