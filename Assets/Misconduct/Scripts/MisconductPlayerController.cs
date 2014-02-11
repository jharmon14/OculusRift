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

public class MisconductPlayerController : MonoBehaviour
{

	// Inspector variables
	public float possLookHeight = 3.0f;
	public float possLerpTime = 1.0f;
	[HideInInspector]
	public MeshRenderer playerStudent;

	// Private variables
	private Transform cam;
	private RaycastHit hit;
	private bool needsRotated = false;
	private bool possessing = false;
	private bool possLerping = false;
	private float possLerp = 0.0f;
	private Vector3 possLerpStart, possLerpEnd;
	private bool possLerpUp = true;
	private bool studentRendererSet = false;
	private int xRotMin, xRotMax, zRotMin, zRotMax;
	private bool xChanged = false;
	private float xPrev;

	void Awake()
	{
		cam = GameObject.Find("CameraRight").transform;
		possLerpStart = this.transform.position;
		possLerpEnd = this.transform.position + new Vector3(0, possLookHeight, 0);
		this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
	}

	// Update is called once per frame
	void Update()
	{
		if (!possessing)
		{
			// Character "leaning" rotation
			this.transform.Rotate(new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")));
			if (this.transform.eulerAngles.x < 314)
			{
				xRotMin = 0;
				xRotMax = 45;
				if (xPrev > 314)
				{
					xChanged = true;
				}
			}
			else
			{
				xRotMin = 315;
				xRotMax = 360;
				if (xPrev < 314)
				{
					xChanged = true;
				}
			}
			if (!xChanged)
			{
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
			}
			this.transform.localEulerAngles = new Vector3(
					Mathf.Clamp(this.transform.eulerAngles.x, xRotMin, xRotMax),
					0,
					Mathf.Clamp(this.transform.eulerAngles.z, zRotMin, zRotMax));
			xPrev = this.transform.eulerAngles.x;
			xChanged = false;
		}

		// Begin Lerp to possession height
		if (Input.GetButtonDown("Possess") && !possLerping)
		{
			possessing = !possessing;
			possLerping = true;
			studentRendererSet = false;
		}

		// Lerp to possession height
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
				}

				possLerping = false;
				Vector3 temp = possLerpStart;
				possLerpStart = possLerpEnd;
				possLerpEnd = temp;
				possLerp = 0;
				possLerpUp = !possLerpUp;
				needsRotated = false;
			}
			
			// Detect player looking at 
			if (Physics.Raycast(cam.position, cam.forward, out hit))
			{
				if (hit.transform.gameObject.name == "StudentShape")
				{
					Debug.Log(hit.transform.parent.name);
				}
			}
		}
	}
}
