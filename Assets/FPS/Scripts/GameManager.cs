/************************************************************************************
 *
 * Filename :   GameManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   (Probably) an OVRPlayerController, tagged enemies
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Inspector variables
	
	
	// Private variables
	
	
	void Awake () {
        // Fade in the camera
        CameraFade.StartAlphaFade(Color.black, true, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
