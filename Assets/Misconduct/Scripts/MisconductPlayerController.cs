/************************************************************************************
 *
 * Filename :   MisconductPlayerController.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MisconductPlayerController : MonoBehaviour
{

	// Inspector variables
	public float possLookHeight = 3.0f;
	public float possLerpTime = 1.0f;
	public float lookDistance = 1.5f;
	[HideInInspector]
	public MeshRenderer playerStudent;

	// Private variables
	private int answersCollected = 0;
	private Transform answersCollectedText;
	private Transform cam;
	private RaycastHit hit;
	private bool needsRotated = false;
	private float lastRaycast = 1.0f;
	private MisconductManager misconductManager;
	private List<Transform> papersCollected = new List<Transform>();
	private Dictionary<Transform, float> papersProgress = new Dictionary<Transform, float>();
	private Transform paperPlayer;
	private bool possessing = false;
	private bool possLerping = false;
	private float possLerp = 0.0f;
	private Vector3 possLerpStart, possLerpEnd;
	private bool possLerpUp = true;
	private MisconductStudent possTarget = null;
	private UISlider slider;
	private Transform sliderTransform;
	private bool studentRendererSet = false;
	private int xRotMin, xRotMax, zRotMin, zRotMax;
	private bool xChanged = false;
	private float xPrev;

	void Awake()
	{
		answersCollectedText = GameObject.Find("PapersCollected").transform;
		cam = GameObject.Find("CameraRight").transform;
		paperPlayer = GameObject.Find("PlayerStudent").transform.FindChild("Paper");
		paperPlayer.FindChild("Indicators").gameObject.SetActive(false);
		Debug.Log(paperPlayer);
		possLerpStart = this.transform.position;
		possLerpEnd = this.transform.position + new Vector3(0, possLookHeight, 0);
		this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
		sliderTransform = GameObject.Find("Progress Bar").transform;
		slider = sliderTransform.GetComponent<UISlider>();
		misconductManager = GameObject.Find("MisconductManager").GetComponent<MisconductManager>();
	}

	// Update is called once per frame
	void Update()
	{
		/*
		 * Sitting character functionality
		 */
		if (!possessing)
		{			
			// Character "leaning" rotation
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
					Mathf.Clamp(this.transform.eulerAngles.z, zRotMin, zRotMax));

			// Detect looking at neighbor's paper
			if (Physics.Raycast(cam.position, cam.forward, out hit, lookDistance))
			{
				if (hit.transform.name == "Paper")
				{
					// add paper if not already tracking
					if (!papersProgress.ContainsKey(hit.transform))
					{
						papersProgress.Add(hit.transform, 0.0f);
					}
					if (!papersCollected.Contains(hit.transform) && (hit.transform != paperPlayer))
					{
						sliderTransform.position = new Vector3(
							hit.transform.position.x,
							hit.transform.position.y + 0.2f,
							hit.transform.position.z);
						sliderTransform.rotation = Quaternion.LookRotation(sliderTransform.position - cam.position);
						slider.sliderValue = papersProgress[hit.transform];
						slider.gameObject.SetActive(true);
						slider.sliderValue += 0.5f * Time.deltaTime;
						papersProgress[hit.transform] = slider.sliderValue;

						// reset the slider and mark answer collected
						if (slider.sliderValue >= 1.0f)
						{
							hit.transform.FindChild("Indicators/Target").gameObject.SetActive(false);
							hit.transform.FindChild("Indicators/Check").gameObject.SetActive(true);
							papersCollected.Add(hit.transform);
							slider.sliderValue = 0.0f;
							misconductManager.collectAnswer();
						}
					}
					else
					{
						// answer already collected message
						slider.gameObject.SetActive(false);
						answersCollectedText.position = hit.transform.position;

					}
				}
				else
				{
					slider.gameObject.SetActive(false);
				}
			}
			else
			{
				slider.gameObject.SetActive(false);
			}
		}


		/*
		 * Begin Lerp to possession height
		 */
		if (Input.GetButtonDown("Possess") && !possLerping && !possessing)
		{				
			// make the top lights invisible
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("ClassroomLights"))
				go.renderer.enabled = false;
			
			possessing = !possessing;
			possLerping = true;
			studentRendererSet = false;
		}


		/*
		 * Lerp to possession begin or end position
		 */
		if (possLerping)
		{
			// Rotate character back to center
			if ((Quaternion.Angle(Quaternion.Euler(Vector3.zero), this.transform.rotation) > 1.0f) && possLerpUp && !needsRotated)
			{
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(Vector3.zero), 5.0f * Time.deltaTime);
			}
			else
			{
				// Start rotating to face down
				if (!needsRotated)
				{
					needsRotated = true;
				}

				// Turn student mesh on when moving up
				if (!studentRendererSet && possLerpUp)
				{
					playerStudent.enabled = !playerStudent.enabled;
					studentRendererSet = true;
				}

				if (needsRotated)
				{
					Vector3 targetRotation = possLerpUp ? new Vector3(90, 0, 0) : Vector3.zero;
					this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(targetRotation), 3.0f * Time.deltaTime);
				}

				// Lerp up from center
				possLerp += Time.deltaTime / possLerpTime;
				this.transform.position = Vector3.Lerp(possLerpStart, possLerpEnd, possLerp);
			}


			// Clean up when Lerp is done
			if (possLerp >= 1.0f)
			{
				// Turn on student mesh when done moving down
				if (!studentRendererSet && !possLerpUp)
				{
					playerStudent.enabled = !playerStudent.enabled;
					studentRendererSet = true;
					GameObject[] students = GameObject.FindGameObjectsWithTag("MisconductStudent") as GameObject[];
					foreach (GameObject student in students)
					{
						student.transform.Find("StudentShape").GetComponent<MeshRenderer>().material.color = Color.white;
					}
					
					// make the top lights visible
					foreach (GameObject go in GameObject.FindGameObjectsWithTag("ClassroomLights"))
						go.renderer.enabled = true;
					
				}

				possLerping = false;
				possLerpStart = possLerpEnd;
				possLerpEnd = possLerpStart + new Vector3(0, possLookHeight, 0);
				possLerp = 0;
				possLerpUp = !possLerpUp;
				needsRotated = false;
			}
		}


		/*
		 * Possessing player functionality
		 */
		if (possessing && !possLerping)
		{			
      if (Time.time - lastRaycast > 0.4f)
			{
				lastRaycast = Time.time;
				if (Physics.Raycast(cam.position, cam.forward, out hit))
				{
					var students = GameObject.FindGameObjectsWithTag("MisconductStudent") as GameObject[];
					var closest = Mathf.Infinity;
					GameObject closestStudent = null;
					foreach (var student in students)
					{
						var distance = (student.transform.position - hit.point).sqrMagnitude;
						if (distance < closest)
						{
							closest = distance;
							closestStudent = student;
						}
					}

					if (possTarget == null)
					{
						possTarget = closestStudent.GetComponent<MisconductStudent>();
						possTarget.highlightColor = true;
					}
					else if (possTarget != closestStudent.GetComponent<MisconductStudent>())
					{
						possTarget.revertColor = true;
						possTarget = closestStudent.GetComponent<MisconductStudent>();
						possTarget.highlightColor = true;
					}
				}
			}

			if ((Input.GetAxis("Possess Pick") == 1) || Input.GetButtonDown("Possess Pick"))
			{				
				possessing = !possessing;
				possLerping = true;
				playerStudent = possTarget.transform.Find("StudentShape").GetComponent<MeshRenderer>();
				possLerpEnd = possTarget.transform.position;
				studentRendererSet = false;
			}
			
			else if (Input.GetButtonDown("Possess Cancel"))
			{
				possessing = !possessing;
				possLerping = true;
				possLerpEnd = possLerpStart + new Vector3(0, -possLookHeight, 0);
				studentRendererSet = false;
			}
		}
	}

	public bool isCheating()
	{
		return slider.gameObject.activeInHierarchy;
	}
}
