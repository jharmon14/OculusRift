using UnityEngine;
using System.Collections;

public class TargetPopup : MonoBehaviour 
{
	public float minDistance = 30.0f;
	
	private Transform player;
	
	// Use this for initialization
	void Start () 
	{
		// start off with the target invisible (not active) and at the proper orientation
		gameObject.renderer.enabled = false;
		gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
		
		hingeJoint.useSpring = true;
		
		// get the player object
		player = GameObject.Find("OVRPlayerControllerWithToggle").transform;

	}
	
	// Update is called once per frame
	void Update () 
	{
		// get the distance from the target and the player
		float distance = Vector3.Distance(transform.position, player.position);
		
		// if you get close enough, enable the target
		if(distance <= minDistance)
		{
			JointSpring tempSpring = new JointSpring();
			tempSpring.spring = 10.0f;
			tempSpring.damper = 2.0f;
			tempSpring.targetPosition = 0.0f;
			
			hingeJoint.spring = tempSpring;

			gameObject.renderer.enabled = true;
		}
	}
}
