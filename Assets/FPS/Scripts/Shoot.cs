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
    private AmmoCount ammoCount;
    private GameObject gameManager;
    private GameManager managerScript;
	
	public AudioClip shootSound;

	void Awake()
	{
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
		fpsManager = GameObject.Find("FPSManager").GetComponent<FPSManager>();
		fpsManager.timeStarted = Time.time;
        ammoCount = GameObject.Find("Panel").GetComponentInChildren<AmmoCount>();
	}

	// Update is called once per frame
	void Update()
	{
        if (managerScript.paused)
            return;

		if (Input.GetButtonDown("Fire1") && !ammoCount.isReloading)
		{
			audio.PlayOneShot(shootSound);
			fpsManager.shotsFired++;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				//Debug.Log(hit.transform.gameObject.name);
				if (hit.transform.gameObject.name == "Target")
				{
					Destroy(hit.transform.gameObject);
					fpsManager.targetsHit++;
				}
				
				else if (hit.transform.gameObject.name == "Civilian")
				{
					Destroy(hit.transform.gameObject);
					fpsManager.civiliansHit++;
				}
			}

		}
	}
}
