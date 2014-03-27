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
	public Transform initialWaypoint;				// starting point of teacher
	public float moveSpeed = 3.0f;					// speed teacher walks
	public float movingTurnSpeed = 20.0f;		// speed teacher turns while walking
	public float minStopTime = 5.0f;				// min time to stop
	public float maxStopTime = 15.0f;				// max time to stop
	public int minWaypointsTilStop = 5;			// min waypoints before stop
	public int maxWaypointsTilStop = 10;		// max waypoints before stop
	public int standingTurns = 2;						// turns to make while standing
	public float standingTurnSpeed = 3.0f;	// speed teacher turns while standing

	// Public variables
	[HideInInspector]
	public float stopTime;									// time teacher will stay stopped

	// Private variables
	private State state = State.Moving;			// state of teacher movement
	// Moving variables
	private Transform lastWaypoint;					// last passed or standing waypoint
	private Transform nextWaypoint;					// next waypoint to move to
	private int waypointsTilStop;						// waypoints to pass before stop
	private int waypointsHit = 0;						// waypoints passed
	// Stopped variables
	private float timeStopped = 0;					// time teacher has been stopped
	private int turnsMade = 0;							// turns teacher has made while stopped
	private Quaternion stopRotation;				// rotation teacher is facing while standing

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
						waypointsHit = 0;
					}
				}
			}
			// Move to the next waypoint
			else
			{
				transform.position = Vector3.Lerp(transform.position, nextWaypoint.position, (moveSpeed * Time.deltaTime) / waypointDistance);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lastWaypoint.position - nextWaypoint.position), movingTurnSpeed);
			}
		}
		// Teacher is stopping to survey the room to stopTime seconds
		else if (state == State.Stopped)
		{
			// Pick a new point to turn to
			if (timeStopped >= ((turnsMade * stopTime)/standingTurns))
			{
				Transform[] waypoints = lastWaypoint.GetComponent<MisconductWaypoint>().waypoints;
				// look in a direction between two waypoints
				if (waypoints.Length > 1)
				{
					Transform way1 = waypoints[Random.Range(0, waypoints.Length)];
					Transform way2;
					do
					{
						way2 = waypoints[Random.Range(0, waypoints.Length)];
					} while (way1 == way2);
					Vector3 lookAt = way1.position + way2.position;
					lookAt.y = 0;
					stopRotation = Quaternion.LookRotation(lookAt);
				}
				else if (waypoints.Length == 1)
				{
					stopRotation = Quaternion.LookRotation(waypoints[0].position);
				}
				else
				{
					stopRotation = transform.rotation;
				}
				turnsMade++;
			}

			if (timeStopped >= stopTime)
			{
				state = State.Moving;
				timeStopped = 0;
				turnsMade = 0;
			}
			else
			{
				timeStopped += Time.deltaTime;
				transform.rotation = Quaternion.RotateTowards(transform.rotation, stopRotation, standingTurnSpeed);
			}
		}
	}
}
