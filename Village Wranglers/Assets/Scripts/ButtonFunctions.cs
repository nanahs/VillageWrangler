using UnityEngine;
using System.Collections;

public class ButtonFunctions : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{

	}
	public void ToGame()
	{
		Application.LoadLevel(4);
	}
	public void ToCredits()
	{
		Application.LoadLevel(1);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void ToMainMenu()
	{
		Application.LoadLevel(0);
	}
}
