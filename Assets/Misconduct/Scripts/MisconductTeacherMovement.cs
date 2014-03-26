/************************************************************************************
 *
 * Filename :   MisconductTeacherMovement.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System;
using System.Collections;

public class MisconductTeacherMovement : MonoBehaviour
{

	// Inspector variables
	public Transform initialWaypoint;
	public float speed = 3.0f;

	// Private variables
	private Transform lastWaypoint;
	private Transform nextWaypoint;
	private System.Random random = new System.Random();

	void Start()
	{
		transform.position = initialWaypoint.position;
		Transform[] waypoints = initialWaypoint.GetComponent<MisconductWaypoint>().waypoints;
		int index = random.Next(waypoints.Length);
		nextWaypoint = waypoints[index];
		lastWaypoint = initialWaypoint;
	}

	// Update is called once per frame
	void Update()
	{
		float waypointDistance = Vector3.Distance(transform.position, nextWaypoint.position);
		if (waypointDistance <= 0.01f)
		{
			Transform[] waypoints = nextWaypoint.GetComponent<MisconductWaypoint>().waypoints;
			Transform pickedWaypoint;
			// Pick a new waypoint that is not the last waypoint
			do
			{
				int index = random.Next(waypoints.Length);
				pickedWaypoint = waypoints[index];
			} while (pickedWaypoint == lastWaypoint);
			lastWaypoint = nextWaypoint;
			nextWaypoint = pickedWaypoint;
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, nextWaypoint.position, (speed * Time.deltaTime) / waypointDistance);
		}
	}
}
