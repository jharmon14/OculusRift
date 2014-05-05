/************************************************************************************
 *
 * Filename :   MisconductPaperIndicators.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class MisconductPaperIndicators : MonoBehaviour {

	// Inspector variables
	
	
	// Private variables
	private Transform targetMarker;	
	private Transform checkMarker;
	private Transform player;
	private bool playerFound = false;
	
	void Start () {
	
	}

	void Awake()
	{
		targetMarker = transform.FindChild("Target");
		checkMarker = transform.FindChild("Check");
		checkMarker.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (!playerFound && (GameObject.Find("Player") != null))
		{
			player = GameObject.Find("Player").transform;
			playerFound = true;
		}
		else if (playerFound)
		{
			transform.rotation = Quaternion.LookRotation(transform.position - player.position);
		}
	}
}
