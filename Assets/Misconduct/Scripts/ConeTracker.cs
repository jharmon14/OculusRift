using UnityEngine;
using System.Collections;

public class ConeTracker : MonoBehaviour {
	
	private bool paused;
	public int suspicionRatePerTick = 1;
	private Transform coneInside;
	private Transform coneOutside;
	private int triggerCatch = 0;
	
	private GameObject teacher;
	private Transform player;

	private GameObject manager;
    private MisconductManager managerScript;
	private MisconductTeacherMovement teacherMovement;
	
	private float startTime = 0.0f;
	private float deltaTime = 0.0f;
	
	public float suspicionTimeTick = 0.25f;
	public float playerSuspicionAngle = 10.0f; 

	// Use this for initialization
	void Awake()
	{
		manager = GameObject.Find("MisconductManager");
        managerScript = manager.GetComponent<MisconductManager>();
		
		player = GameObject.Find("Player").transform;

		coneInside = GameObject.Find("Cone_Inside").transform;
		coneOutside = GameObject.Find("Cone_Outside").transform;

		teacher = GameObject.Find ("Teacher");
		teacherMovement = teacher.GetComponent<MisconductTeacherMovement>();
	}

	void OnTriggerEnter(Collider trigger)
	{

		if (trigger.gameObject.tag == "Cone")
		{
			// Change material to reflect Player action
			updateConeColor(Color.yellow);
			
			// Check if player is supicious
			if(isPlayerSuspicious()){
				setTeacherPath(false);
				
				// Time since game started
				startTime = Time.time;

			}

		}
	}

	void OnTriggerStay(Collider trigger)
	{

		if (trigger.gameObject.tag == "Cone")
		{
			if(isPlayerSuspicious()){
				
				deltaTime = Time.time - startTime;

				if(deltaTime > suspicionTimeTick){
					// Keep incrementing suspicion if Player is actin' a fool
					managerScript.increaseSuspicion(suspicionRatePerTick);
					
					// Reset Time
					startTime = Time.time;
					deltaTime = 0;
				}
				
				
				updateConeColor(Color.red);
				
			} else {
				updateConeColor(Color.yellow);
				setTeacherPath(true);
	
			}			
		}
	}

	void OnTriggerExit(Collider trigger)
	{
		if (trigger.gameObject.tag == "Cone")
		{
			// Change material to reflect Player action
			updateConeColor(Color.green);

		}
	}
	
	void updateConeColor(Color color){
		coneInside.transform.GetComponent<MeshRenderer>().material.color = color;
		coneOutside.transform.GetComponent<MeshRenderer>().material.color = color;
	}
	
	void setTeacherPath(bool setBool){
		if(setBool){
			teacherMovement.enabled = true;
		} else {
			teacherMovement.enabled = false;
		}
	}
	
	bool isPlayerSuspicious(){
		Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
		float angle = Quaternion.Angle(player.rotation, targetRotation);
		
		if(angle > playerSuspicionAngle){
			return true;
		} else {
			return false;
		}
		
	}

	void Update(){

	}
}
