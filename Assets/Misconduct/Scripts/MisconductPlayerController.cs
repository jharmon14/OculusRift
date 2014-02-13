using UnityEngine;
using System.Collections;

public class MisconductPlayerController : MonoBehaviour 
{
  // Inspector variables
	public float possLookHeight = 8.0f;
	public float movingLerpTime = 1.0f;
	public float raycastRate = 0.5f;

	// Public variables
	[HideInInspector]
	public MeshRenderer playerStudent;

  // Private variables
	private enum State
	{
		Sitting = 0,
    Centering,
		Moving,
		Possessing
	};
	private Transform cam;
	private State state = State.Sitting;

	// Sitting variables
	private int xRotMin, xRotMax, zRotMin, zRotMax;

  // Moving variables
	private bool movingUp, movingDown = false;
	private float movingLerp = 0.0f;

  // Possession variables
	private float lastRaycast;
	private Vector3 possCancel;
	private Vector3 possLook;
	private MisconductStudent possStudent;
	private Vector3 possTarget;

	// Use this for initialization
	void Awake() 
	{
		cam = GameObject.Find("CameraRight").transform;
		SittingSetup();
	}
	
	// Update is called once per frame
	void Update()
	{
    // player is sitting at a desk
		if (state == State.Sitting)
		{
      // character leaning
			this.transform.Rotate(new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")));
      if (this.transform.eulerAngles.x < 314)
			{
				xRotMin = 0;
				xRotMax = 45;
			}
			else
			{
				xRotMin = 315;
				xRotMax = 360;
			}
			if (this.transform.eulerAngles.z < 50)
			{
				zRotMin = 0;
				zRotMax = 45;
			}
			else
			{
				zRotMin = 315;
				zRotMax = 360;
			}
			this.transform.localEulerAngles = new Vector3(
					Mathf.Clamp(this.transform.eulerAngles.x, xRotMin, xRotMax),
					0,
					Mathf.Clamp(this.transform.eulerAngles.z, zRotMin, zRotMax)
					);

      // start possessing
			if (Input.GetButtonDown("Possess"))
			{
				state = State.Centering;
			}
		}

		// Center the player from the sitting position before moving up
		else if (state == State.Centering)
		{
			if (Quaternion.Angle(Quaternion.Euler(Vector3.zero), this.transform.rotation) > 1.0f)
			{
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(Vector3.zero), 5.0f * Time.deltaTime);
			}
			else
			{
				movingUp = true;
				playerStudent.enabled = true;
				state = State.Moving;
			}
		}

		// Move the player to possession spot
		else if (state == State.Moving)
		{
      // Lerp up to look position
			if (movingUp)
			{
				if (movingUp && (movingLerp < 1.0f))
				{
					this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(90, 0, 0), 3.0f * Time.deltaTime);
					movingLerp += Time.deltaTime / movingLerpTime;
					this.transform.position = Vector3.Lerp(possCancel, possLook, movingLerp);
				}
				else
				{
					movingLerp = 0.0f;
					movingUp = false;
					state = State.Possessing;
				}
			}
			else if (movingDown)
			{
				// Lerp down to target position
				if (movingDown && (movingLerp < 1.0f))
				{
					this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), 3.0f * Time.deltaTime);
					movingLerp += Time.deltaTime / movingLerpTime;
					this.transform.position = Vector3.Lerp(possLook, possTarget, movingLerp);
				}
				else
				{
					movingLerp = 0.0f;
					movingDown = false;
					SittingSetup();
					playerStudent.enabled = false;
					state = State.Sitting;
				}
			}
		}

		// Player can choose a student to possess
		else if (state == State.Possessing)
		{
      // Only find student after seconds
			if (Time.time - lastRaycast > raycastRate)
			{
				// Find nearest student to possess
        RaycastHit hit;
				if (Physics.Raycast(cam.position, cam.forward, out hit))
				{
					GameObject[] students = GameObject.FindGameObjectsWithTag("MisconductStudent") as GameObject[];
					float closest = Mathf.Infinity;
					GameObject closestStudent = null;
					foreach (GameObject student in students)
					{
						float distance = (student.transform.position - hit.point).sqrMagnitude;
						if (distance < closest)
						{
							closest = distance;
							closestStudent = student;
						}
					}

					possTarget = closestStudent.transform.position;

          // highlight selected student
					if (possStudent == null)
					{
						possStudent = closestStudent.GetComponent<MisconductStudent>();
						possStudent.highlightColor = true;
					}
					else if (possStudent != closestStudent.GetComponent<MisconductStudent>())
					{
						possStudent.revertColor = true;
						possStudent = closestStudent.GetComponent<MisconductStudent>();
						possStudent.highlightColor = true;
					}

					lastRaycast = Time.time;
				}
			}

      // Possess the selected student
			if (Input.GetAxis("Right Trigger") == 1)
			{
				movingDown = true;
				foreach (var student in GameObject.FindGameObjectsWithTag("MisconductStudent") as GameObject[])
				{
					student.transform.Find("StudentShape").GetComponent<MeshRenderer>().material.color = Color.white;
				}
				possStudent.revertColor = true;
				playerStudent = possStudent.transform.Find("StudentShape").GetComponent<MeshRenderer>();
				state = State.Moving;
        return;
			}

      // Cancel possession and return to previous body
			if (Input.GetButtonDown("Possess"))
			{
				movingDown = true;
				possStudent.revertColor = true;
				possTarget = possCancel;
				state = State.Moving;
				return;
			}
		}

		else
		{
			Debug.Log("You dun fouled up.");
		}
	}

	void SittingSetup()
	{
		possCancel = this.transform.position;
		possLook = possCancel + new Vector3(0, possLookHeight, 0);
		possStudent = null;
	}
}
