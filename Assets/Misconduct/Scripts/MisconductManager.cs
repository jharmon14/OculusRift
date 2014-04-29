/************************************************************************************
 *
 * Filename :   MisconductManager.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class MisconductManager : MonoBehaviour
{

	// Inspector variables
	public GameObject playerOject;
  [HideInInspector]
	public MisconductPlayerController player;
	[HideInInspector]
	public MisconductStudent playerStudent;

	private int answersCollected = 0;
	private int suspicionLevel = 0;

	// Private variables

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}
	
	public void increaseSuspicion(int val = 1){
		suspicionLevel += val;
	}
	
	public void reduceSuspicion(int val = 1){
		suspicionLevel -= val;
		if(suspicionLevel < 0)
			suspicionLevel = 0;
	}

	public void collectAnswer()
	{
		answersCollected++;
	}
}
