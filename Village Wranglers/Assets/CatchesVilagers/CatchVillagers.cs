using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatchVillagers : MonoBehaviour
{
	public GameObject count;
	int score;
	// Use this for initialization
	void Start()
	{
		score = 00;
	}

	// Update is called once per frame
	void Update()
	{
		if (score < 10)
			count.GetComponent<Text>().text = '0' + score.ToString();
		else
			count.GetComponent<Text>().text = score.ToString();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.GetComponent<VillagerController>())
			score++;
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.GetComponent<VillagerController>())
			score--;
	}
}
