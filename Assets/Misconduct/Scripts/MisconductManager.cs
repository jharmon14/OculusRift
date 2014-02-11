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

	// Private variables

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}
}
