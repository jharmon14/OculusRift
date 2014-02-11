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

	// Private variables
	private Transform cam;
	private bool possessing = false;
	private bool possLerping = false;
	private float possLerp = 0.0f;
	private Vector3 possLerpStart, possLerpEnd;
	private int xRotMin, xRotMax, zRotMin, zRotMax;
	private bool xChanged = false;
	private float xPrev;

	void Awake()
	{
		cam = transform.Find("OVRCameraController").transform;
		possLerpStart = cam.position;
		possLerpEnd = cam.position + new Vector3(0, possLookHeight, 0);
	}

	// Update is called once per frame
	void Update()
	{
		if (!possessing)
		{
      // Character "leaning" rotation
			transform.Rotate(new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")));
			if (transform.eulerAngles.x < 314)
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
				if (transform.eulerAngles.z < 50)
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
			transform.localEulerAngles = new Vector3(
					Mathf.Clamp(transform.eulerAngles.x, xRotMin, xRotMax),
          0,
					Mathf.Clamp(transform.eulerAngles.z, zRotMin, zRotMax));
			xPrev = transform.eulerAngles.x;
			xChanged = false;
		}

    // Begin Lerp to possession height
		if (Input.GetButtonDown("Possess") && !possLerping)
		{
			possessing = !possessing;
			possLerping = true;
		}

    // Lerp to possession height
		if (possLerping)
		{
			// Rotate character back to center
			if (Quaternion.Angle(Quaternion.Euler(Vector3.zero), transform.rotation) > 0.1)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 5.0f * Time.deltaTime);
			}
      else
			{
        // Lerp up from center
				possLerp += Time.deltaTime / possLerpTime;
				cam.position = Vector3.Lerp(possLerpStart, possLerpEnd, possLerp);
			}

			if (possLerp >= 1.0f)
			{
				possLerping = false;
				Vector3 temp = possLerpStart;
				possLerpStart = possLerpEnd;
				possLerpEnd = temp;
				possLerp = 0;
			}
		}
	}
}
