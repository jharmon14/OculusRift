/************************************************************************************
 *
 * Filename :   Transport.cs
 * Content  :   
 * Expects  :   
 * Authors  :   Ryan Copeland
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour {

    // Inspector variables
    public float maxDistance = 40.0f;

    // Private variables
    private Transform arcade;

	// Use this for initialization
	void Start () 
	{
        arcade = GameObject.Find("ArcadeOrigin").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		float distance = Vector3.Distance (transform.position, arcade.position);
		
		// check for positive/negative values so you would add or subtract to reduce the glitch of the player
		// constantly teleporting
		
		if(distance > maxDistance)
		{
			// if you are going in from the "left" (x positive -> x negative)
	        if (transform.position.x > transform.position.z && transform.position.x > 0)
			    transform.position = new Vector3(-transform.position.x + 3, transform.position.y, -transform.position.z);
			
			// if you are going in from the "right" (x negative -> x positive)
	        else if (transform.position.x < transform.position.z && transform.position.x < 0)
			    transform.position = new Vector3(-transform.position.x - 3, transform.position.y, -transform.position.z);
			
			// if you are going in from the "bottom" (z positive -> z negative)
	        else if (transform.position.z > transform.position.x && transform.position.z > 0)
			    transform.position = new Vector3(-transform.position.x, transform.position.y, -transform.position.z + 3);
			
			// if you are going in from the "top" (z negative -> z positive)
	        else if (transform.position.z < transform.position.x && transform.position.z < 0)
			    transform.position = new Vector3(-transform.position.x, transform.position.y, -transform.position.z - 3);
			
			
		}
		
	}
}
