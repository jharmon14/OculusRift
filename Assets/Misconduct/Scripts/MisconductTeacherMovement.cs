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
	public float standingHeadAngle = 30.0f;
	public float movingHeadAngle = 10.0f;
	public float standingHeadIncrement = 0.25f;
	public float movingHeadIncrement = 0.5f;

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
	// Head variables
	private Transform head;
	private bool headLeft = true;
	private float headAngle = 0;

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

	void Awake()
	{
		head = GameObject.FindGameObjectWithTag("MisconductTeacherHead").transform;
	}

	// Update is called once per frame
	void Update()
	{
		// Teacher is moving between waypoints
		if (state == State.Moving)
		{
			UpdateMoving();
		}
		// Teacher is stopping to survey the room to stopTime seconds
		else if (state == State.Stopped)
		{
			UpdateStopped();
		}
		// Rotate teacher's head to scan the class
		UpdateHead();
	}

	// Teacher is moving between waypoints
	void UpdateMoving()
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
			} 
			while (pickedWaypoint == lastWaypoint);
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
			Quaternion lookRotation = Quaternion.LookRotation(
				lastWaypoint.position - nextWaypoint.position
				);
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation, 
				lookRotation, 
				movingTurnSpeed
				);
			transform.position = Vector3.Lerp(
				transform.position, 
				nextWaypoint.position, 
				(moveSpeed * Time.deltaTime) / waypointDistance
				);
		}
	}

	// Teacher is stopping to survey the room to stopTime seconds
	void UpdateStopped()
	{
		// Pick a new point to turn to
		if (timeStopped >= ((turnsMade * stopTime) / standingTurns))
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
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation, 
				stopRotation, 
				standingTurnSpeed
				);
		}
	}

	// Rotate teacher's head to scan the class
	void UpdateHead()
	{
		float angle = state == State.Moving ? movingHeadAngle : standingHeadAngle;
		float headIncrement = state == State.Moving 
			? movingHeadIncrement 
			: standingHeadIncrement;
		angle = headLeft ? -angle : angle;
		headIncrement = headLeft ? -headIncrement : headIncrement;
		head.Rotate(Vector3.up, headIncrement);
		headAngle += headIncrement;
		if (headLeft && (headAngle <= angle))
		{
			headLeft = false;
		}
		else if (!headLeft && (headAngle >= angle))
		{
			headLeft = true;
		}
	}
}
