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
        arcade = GameObject.FindGameObjectWithTag("Arcade").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		float distance = Vector3.Distance (transform.position, arcade.position);

        if (distance > maxDistance)
		    transform.position = new Vector3(-transform.position.x, transform.position.y, -transform.position.z);
	}
}
