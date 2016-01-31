using UnityEngine;
using System.Collections;

public class TrapBehaviors : MonoBehaviour
{

	public enum TrapTypes { SpikeTrap, BearTrap, TarPit };
	public GameObject trap;
	float counter;
	bool stopped;
	bool slowed;
	GameObject player;
	public float movespeed;
	// Use this for initialization
	void Start()
	{
		counter = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (counter <= 3.0f)
			counter += Time.deltaTime;
		if (counter >= 3.0f)
		{
			stopped = false;
			slowed = false;
		}

		if (slowed)
			player.GetComponent<PlayerController>().moveSpeed = movespeed;
		else
			player.GetComponent<PlayerController>().moveSpeed = movespeed * 2;



		if (stopped)
			player.transform.position.Set(trap.transform.position.x, trap.transform.position.y, trap.transform.position.z);
	}

	void OnTriggerEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			player = col.gameObject;
			if (trap.GetComponent<TrapTypes>() == TrapTypes.SpikeTrap)
			{
				col.gameObject.SetActive(false);
			}
			if (trap.GetComponent<TrapTypes>() == TrapTypes.BearTrap)
			{
				counter = 0;
				stopped = true;
			}
			if (trap.GetComponent<TrapTypes>() == TrapTypes.TarPit)
			{
				counter = 0;
				slowed = true;
			}
		}
	}
}
