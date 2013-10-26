/************************************************************************************
 *
 * Filename :   StartGame.cs
 * Content  :   Start arcade game the player is looking at
 * Expects  :   Arcade machine with "StartButton" tag and StartButton script
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Inspector variables
    public float minDistanceToStartGame = 1.0f;
	
	// Private variables
    private Transform cameraPos;
	
	void Start () {
        cameraPos = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        // Player presses fire1 button
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraPos.position, cameraPos.forward, out hit))
            {
                if ((hit.transform.gameObject.tag == "StartButton") && (hit.distance < minDistanceToStartGame))
                {
                    StartButton sb = hit.transform.gameObject.GetComponent("StartButton") as StartButton;
                    // Fade out the camera and load the game level based on the machine they're looking at
                    CameraFade.StartAlphaFade(Color.black, false, 2.0f, 2.0f, () => { Application.LoadLevel(sb.sceneName); });
                }
            }
        }
	}
}
