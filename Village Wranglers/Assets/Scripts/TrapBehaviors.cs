using UnityEngine;
using System.Collections;

	public enum TrapTypes { LavaTrap, BearTrap, TarPit };
public class TrapBehaviors : MonoBehaviour
{
	public TrapTypes type;
	public GameObject trap;
	float counter;
	bool stopped;
	bool slowed;
	public GameObject player;
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
			player.SetActive(true);
		}

		if (slowed)
			player.GetComponent<PlayerController>().moveSpeed = movespeed;
		else
			player.GetComponent<PlayerController>().moveSpeed = movespeed * 2;

		if (stopped)
			player.transform.position.Set(trap.transform.position.x, trap.transform.position.y, trap.transform.position.z);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			player = col.gameObject;
			if (trap.GetComponent<TrapBehaviors>().type == TrapTypes.LavaTrap)
			{
				col.gameObject.SetActive(false);
				counter = 0;
			}
			if (trap.GetComponent<TrapBehaviors>().type == TrapTypes.BearTrap)
			{
				counter = 0;
				stopped = true;
			}
			if (trap.GetComponent<TrapBehaviors>().type == TrapTypes.TarPit)
			{
				counter = 0;
				slowed = true;
			}
		}
	}
}
