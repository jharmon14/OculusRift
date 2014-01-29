/************************************************************************************
 *
 * Filename :   OverworldManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   (Probably) an OVRPlayerController, tagged enemies
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class OverworldManager : MonoBehaviour
{

    // Inspector variables
    public GameObject pauseMenu;
    public GameObject forwardDirection;
    // Private variables
    public bool paused = false;

    public void Start()
    {
        // Make sure pause menu is not active
        pauseMenu.SetActive(false);
    }

    void Awake()
    {
        // Fade in the camera
        CameraFade.StartAlphaFade(Color.black, true, 4.0f);
    }

    public void TogglePause()
    {
        paused = !paused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
    }
}