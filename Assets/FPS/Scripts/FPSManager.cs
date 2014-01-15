/************************************************************************************
 *
 * Filename :   FPSManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   (Probably) an OVRPlayerController, tagged enemies
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class FPSManager : MonoBehaviour 
{
    public int targetsHit = 0;
    public int shotsFired = 0;

	void Awake() 
    {
        // Fade in the camera
        CameraFade.StartAlphaFade(Color.black, true, 4.0f);
	}
	
	// Update is called once per frame
	void Update() 
    {
	}
}