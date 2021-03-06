﻿using UnityEngine;
using System.Collections;

public class FireGun : MonoBehaviour {
	
	public GameObject gunSight;
	private int shotsHit = 0;
	private int shotsTotal = 0;
	private RaycastHit hit;
    private AmmoCount ammoCount;
    private GameObject go;


    void Start()
    {
        go = GameObject.Find("Panel");
        ammoCount = go.GetComponent<AmmoCount>();
    }
	// Update is called once per frame
	void Update () {
    	
		//Debug.Log(ammoCount.isReloading);
		
		Vector3 fwd = gunSight.gameObject.transform.TransformDirection(Vector3.forward);
		
		// If Raycast hits something, and that something is an enemy
		if( Physics.Raycast(transform.position, fwd, out hit, 200) && hit.collider.tag == "Enemy"){
			
			//DEBUG: Directional light is Green when raytrace hits
	        gunSight.gameObject.light.color = Color.green;
			//Debug.Log ("Hit");
			
			// Shot Hit
			if(!ammoCount.isReloading && Input.GetButtonDown("Fire1")){
				shotsHit++;
				shotsTotal++;
	            Debug.Log("Accuracy: " + shotsHit + "/" + shotsTotal);
			}
		} else {
			
			//DEBUG: Directional light is Red when raytrace misses
			gunSight.gameObject.light.color = Color.red;
			
			// Shot Missed
			if (Input.GetButtonDown("Fire1")){
				shotsTotal++;
				Debug.Log("Accuracy: " + shotsHit + "/" + shotsTotal);
			}
		}
    }
}
