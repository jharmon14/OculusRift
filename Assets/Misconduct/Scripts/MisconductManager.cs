﻿/************************************************************************************
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

	// Private variables

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void collectAnswer()
	{
		answersCollected++;
	}
}
