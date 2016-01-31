using UnityEngine;
using System.Collections;


public class TrapBehaviors : MonoBehaviour
{
	public GameObject player;
	public float holdTime = 2f;

	IEnumerator holdPlayer()
	{
		while (true)
		{
			player.transform.position = this.transform.position;
			yield return null;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			player = col.gameObject;
			StartCoroutine("holdPlayer");
			Destroy(this.gameObject, holdTime);
		}
	}
}
