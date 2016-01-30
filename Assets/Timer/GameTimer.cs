using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{

	float Gametime;
	int minutes;
	int seconds;
	public Text timer;
	// Use this for initialization
	void Start()
	{
		Gametime = 3 * 60; // three minutes


	}

	// Update is called once per frame
	void Update()
	{
		minutes = (int)(Gametime / 60.0f);
		seconds = (int)(Gametime % 60.0f);
		timer.text = minutes.ToString() + ':' + seconds.ToString();
		

		if (Gametime < 0)
		{
			// exit the game here!
		}
		Gametime -= Time.deltaTime;
	}
}
