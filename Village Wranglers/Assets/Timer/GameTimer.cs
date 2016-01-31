using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{

	public float Gametime;
	int minutes;
	int seconds;
	public Text timer;
	float colorCounter;
	public GameObject p1score;
	public GameObject p2score;

	// Use this for initialization
	void Start()
	{
		Gametime = 3 * 60; // three minutes
		colorCounter = 0;
	}

	// Update is called once per frame
	void Update()
	{
		minutes = (int)(Gametime / 60.0f);
		seconds = (int)(Gametime % 60.0f);
		if (seconds < 10)
			timer.text = minutes.ToString() + ":0" + seconds.ToString();
		else
			timer.text = minutes.ToString() + ':' + seconds.ToString();

		// needs tuning
		if (Gametime <= 30 && Gametime > 10)
			timer.color = Color.red;
		else if (Gametime <= 10 && timer.color == Color.red && colorCounter > 0.4f)
			timer.color = Color.black;
		else if (Gametime <= 10 && timer.color == Color.black && colorCounter > 0.4f)
			timer.color = Color.red;
		else if (Gametime > 30)
			timer.color = Color.black;

		if (Gametime < 0)
		{
			// exit the game here!
			if (p1score.GetComponent<CatchVillagers>().score == p2score.GetComponent<CatchVillagers>().score)
				Application.LoadLevel(5);
			else if (p1score.GetComponent<CatchVillagers>().score > p2score.GetComponent<CatchVillagers>().score)
				Application.LoadLevel(3);
			else
				Application.LoadLevel(2);

		}
		if (colorCounter > 0.5f)
			colorCounter = 0;
		Gametime -= Time.deltaTime;
		colorCounter += Time.deltaTime;
	}
}
