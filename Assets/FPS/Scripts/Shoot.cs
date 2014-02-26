/************************************************************************************
 *
 * Filename :   Shoot.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    // Private variables
    private FPSManager fpsManager;
    private RaycastHit hit;
    private GameObject pauseManager;
    private PauseManagerScript managerScript;
    private bool paused;


    void OnLevelWasLoaded(int level)
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        fpsManager = GameObject.Find("FPSManager").GetComponent<FPSManager>();
        fpsManager.timeStarted = Time.time;
    }

	void Awake()
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        fpsManager = GameObject.Find("FPSManager").GetComponent<FPSManager>();
        fpsManager.timeStarted = Time.time;
	}
	
	// Update is called once per frame
	void Update()
    {
        paused = managerScript.paused;
        if (paused)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            fpsManager.shotsFired++;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.name == "Target")
                {
                    Destroy(hit.transform.gameObject);
                    fpsManager.targetsHit++;
                }
            }
        }
	}
}
