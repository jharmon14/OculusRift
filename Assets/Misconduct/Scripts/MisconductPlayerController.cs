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

public class MisconductPlayerController : MonoBehaviour {

	// Inspector variables
    public int xRotMin, xRotMax, zRotMin, zRotMax;
    public Transform lookPos;
	
	// Private variables
    private bool xChanged = false;
    private float xPrev;

    void Awake()
    {
        lookPos = GameObject.Find("Possession Look Point").transform;
    }
	
	// Update is called once per frame
	void Update () {
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
            Mathf.Clamp(transform.eulerAngles.y, 0.0f, 0.0f),
            Mathf.Clamp(transform.eulerAngles.z, zRotMin, zRotMax));
        xPrev = transform.eulerAngles.x;
        xChanged = false;

        if (Input.GetButtonDown("Possess"))
        {
            var cam = transform.Find("OVRCameraController").transform;
            cam.position = lookPos.position;
        }
    }
}
