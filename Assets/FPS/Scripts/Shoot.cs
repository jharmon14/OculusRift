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
	
	void Awake()
    {
        fpsManager = GameObject.Find("FPSManager").GetComponent<FPSManager>();
        fpsManager.timeStarted = Time.time;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fpsManager.shotsFired++;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.gameObject.name == "Target")
                {
                    Destroy(hit.transform.gameObject);
                    fpsManager.targetsHit++;
                }
            }
        }
	}
}
