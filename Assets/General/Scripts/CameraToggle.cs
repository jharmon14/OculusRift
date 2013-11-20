/************************************************************************************
 *
 * Filename :   CameraToggle.cs
 * Content  :   OVR / First Person toggle.
 *              This script is an addon for the OVRPlayerController that incorporates
 *              another camera and allows the player to toggle between standard and
 *              rift camera by pushing F1.
 *              Primarily intended to ease development.
 * Expects  :   OVRPlayerController from Oculus Unity Package
 *              Another main camera as a child of Forward Direction
 *              Standard Unity MouseLook script on OVRPlayerController object
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class CameraToggle : MonoBehaviour {

    // Inspector variables
    public Transform leftOVRCamera;
    public Transform rightOVRCamera;
    public Transform firstPersonCamera;
	
	
	public bool startWithRift = true; // starts with rift on/off
	
    // Private variables
    private bool ovrOn; // rift camera on = true, off = false

	void Start () 
    {
		if(startWithRift){
			// Start with toggle on OVR camera
        	firstPersonCamera.gameObject.SetActive(false);
        	ovrOn = true;
		} else {
			rightOVRCamera.gameObject.SetActive(false);
			leftOVRCamera.gameObject.SetActive (false);
			ovrOn = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Press F1 to switch between cameras
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ovrOn = !ovrOn;
            leftOVRCamera.gameObject.SetActive(ovrOn);
            rightOVRCamera.gameObject.SetActive(ovrOn);
            GetComponent<OVRGamepadController>().enabled = ovrOn;
            firstPersonCamera.gameObject.SetActive(!ovrOn);
        }
	}
}
