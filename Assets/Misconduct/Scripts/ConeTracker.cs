using UnityEngine;
using System.Collections;

public class ConeTracker : MonoBehaviour {
	
	private bool paused;
	public int suspicionLevel = 0;
	private Transform coneInside;
	private Transform coneOutside;
	private int triggerCatch = 0;

	private GameObject manager;
    private MisconductManager managerScript;

	// Use this for initialization
	void Awake()
	{
		manager = GameObject.Find("MisconductManager");
        managerScript = manager.GetComponent<MisconductManager>();
		coneInside = GameObject.Find("Cone_Inside").transform;
		coneOutside = GameObject.Find("Cone_Outside").transform;
	}

	void OnTriggerEnter(Collider trigger)
	{

		if (trigger.gameObject.tag == "Cone")
		{
			// Change material to reflect Player action
			updateConeColor(Color.yellow);
			
			// Stop Pathing and Head Movement

		}
	}

	void OnTriggerStay(Collider trigger)
	{
		/*
        if (paused)
        {
            return;
        }*/
		if (trigger.gameObject.tag == "Cone")
		{
			updateConeColor(Color.red);

			// Keep incrementing suspicion if Player is actin' a fool
			managerScript.increaseSuspicion();
		}
	}

	void OnTriggerExit(Collider trigger)
	{
		if (trigger.gameObject.tag == "Cone")
		{
			// Change material to reflect Player action
			updateConeColor(Color.green);
			
			// Start Pathing and Head Movement
		}
	}
	
	void updateConeColor(Color color){
		coneInside.transform.GetComponent<MeshRenderer>().material.color = color;
		coneOutside.transform.GetComponent<MeshRenderer>().material.color = color;
	}

	void Update(){
		/*
				paused = managerScript.paused;
				if (paused)
				{
						return;
				}
		*/
	}
}
