/************************************************************************************
 *
 * Filename :   MisconductTeacherMovement.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class MisconductTeacherMovement : MonoBehaviour
{

	// Inspector variables
	public Transform initialWaypoint;
	public float speed = 3.0f;
	public float minStopTime = 5.0f;
	public float maxStopTime = 15.0f;
	public int minWaypointsTilStop = 5;
	public int maxWaypointsTilStop = 10;

	// Private variables
	private Transform lastWaypoint;
	private Transform nextWaypoint;
	private float stopTime;
	private float timeStopped;
	private int waypointsTilStop;
	private int waypointsHit = 0;
	private State state = State.Moving;

	public enum State
	{
		Stopped = 0,
		Moving
	}

	void Start()
	{
		transform.position = initialWaypoint.position;
		Transform[] waypoints = initialWaypoint.GetComponent<MisconductWaypoint>().waypoints;
		int index = Random.Range(0, waypoints.Length);
		nextWaypoint = waypoints[index];
		lastWaypoint = initialWaypoint;
		waypointsTilStop = Random.Range(minWaypointsTilStop, maxWaypointsTilStop);
	}

	// Update is called once per frame
	void Update()
	{
		if (state == State.Moving)
		{
			float waypointDistance = Vector3.Distance(transform.position, nextWaypoint.position);
			if (waypointDistance <= 0.01f)
			{
				Transform[] waypoints = nextWaypoint.GetComponent<MisconductWaypoint>().waypoints;
				Transform pickedWaypoint;
				// Pick a new waypoint that is not the last waypoint
				do
				{
					int index = Random.Range(0, waypoints.Length);
					pickedWaypoint = waypoints[index];
				} while (pickedWaypoint == lastWaypoint);
				lastWaypoint = nextWaypoint;
				nextWaypoint = pickedWaypoint;
				waypointsHit++;

				// Check if enough waypoints have been hit to stop
				if (waypointsHit >= waypointsTilStop)
				{
					MisconductWaypoint waypoint = lastWaypoint.GetComponent<MisconductWaypoint>();
					// Only stop on allowed waypoints
					if (waypoint.canWaitAt)
					{
						state = State.Stopped;
						stopTime = Random.Range(minStopTime, maxStopTime);
					}
				}
			}
			// Move to the next waypoint
			else
			{
				transform.position = Vector3.Lerp(transform.position, nextWaypoint.position, (speed * Time.deltaTime) / waypointDistance);
			}
		}
		// Teacher is stopping to survey the room to stopTime seconds
		else if (state == State.Stopped)
		{
			if (timeStopped >= stopTime)
			{
				state = State.Moving;
				timeStopped = 0;
			}
			else
			{
				timeStopped += Time.deltaTime;
			}
		}
	}
}
