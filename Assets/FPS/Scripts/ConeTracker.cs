using UnityEngine;
using System.Collections;

public class ConeTracker : MonoBehaviour {
	
	private bool paused;
	public int suspicionLevel = 0; 
	private Transform coneInside;
	private Transform coneOutside;
	
	// Use this for initialization
	void Start () {
        //manager = GameObject.Find("GameManager");
        //managerScript = manager.GetComponent<GameManager>();
		coneInside = GameObject.Find("Cone_Inside").transform;
		coneOutside = GameObject.Find("Cone_Outside").transform;
	}
	
	void OnTriggerEnter(Collider trigger) {
		/*
        if (paused)
        {
            return;
        }*/
        if(trigger.gameObject.tag == "Cone"){;
			// Change material to reflect Player action
			Debug.Log("ENTERED!!?!?!?!?!??!");
			coneInside.transform.GetComponent<MeshRenderer>().material.color = Color.yellow;
			coneOutside.transform.GetComponent<MeshRenderer>().material.color = Color.yellow;
			
		}
    }
	
	void OnTriggerStay(Collider trigger){
		/*
        if (paused)
        {
            return;
        }*/
		if(trigger.gameObject.tag == "Cone"){
			// Keep incrementing suspicion if Player is actin' a fool
			// suspicionLevel++;
		}
	}
	
	void OnTriggerExit(Collider trigger){
		if(trigger.gameObject.tag == "Cone"){;
			// Change material to reflect Player action
			coneInside.transform.GetComponent<MeshRenderer>().material.color = Color.green;
			coneOutside.transform.GetComponent<MeshRenderer>().material.color = Color.green;
			
		}
	}
	
	/*
	void Update(){
        paused = managerScript.paused;
        if (paused)
        {
            return;
        }	
	}*/
}
