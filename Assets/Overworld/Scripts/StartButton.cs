/************************************************************************************
 *
 * Filename :   StartButton.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{

	// Inspector variables
	public GameManager.Levels level;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<StartGame>().level = level;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<StartGame>().level = GameManager.Levels.None;
		}
	}
}