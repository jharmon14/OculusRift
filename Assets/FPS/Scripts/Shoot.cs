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
    private bool reloading;
    private AmmoCount ammoCountScript;

    void OnLevelWasLoaded(int level)
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        fpsManager = GameObject.Find("FPSManager").GetComponent<FPSManager>();
        fpsManager.timeStarted = Time.time;
        ammoCountScript = GameObject.Find("Panel").GetComponentInChildren<AmmoCount>();
    }

	void Awake()
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        fpsManager = GameObject.Find("FPSManager").GetComponent<FPSManager>();
        fpsManager.timeStarted = Time.time;
        ammoCountScript = GameObject.Find("Panel").GetComponentInChildren<AmmoCount>();
	}
	
	// Update is called once per frame
	void Update()
    {
        paused = managerScript.paused;
        if (paused)
            return;

        reloading = ammoCountScript.reloading;
        if (Input.GetButtonDown("Fire1") && !reloading)
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
