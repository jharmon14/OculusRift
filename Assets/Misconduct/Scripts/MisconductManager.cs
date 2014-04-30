/************************************************************************************
 *
 * Filename :   MisconductManager.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class MisconductManager : MonoBehaviour
{

	// Inspector variables
	public GameObject playerObject;
	public int maxSuspicion = 100;

  [HideInInspector]
	public MisconductPlayerController player;
	[HideInInspector]
	public MisconductStudent playerStudent;
	
	private UISlider slider;
	private int answersCollected = 0;
	private int answersTotal = 0;
	private float suspicionLevel = 0;
	private UILabel answersCollectedText;


	// Private variables

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}
	
	void Awake(){
		GameObject[] papers = GameObject.FindGameObjectsWithTag("Paper");
		answersTotal = papers.Length - 1; // minus one for the player's paper
		answersCollectedText = transform.FindChild("PapersCollected").GetComponent<UILabel>();
		answersCollectedText.text = answersCollected + "\nof\n" + answersTotal;
		slider = GameObject.Find("SuspicionProgressBar").GetComponent<UISlider>();
	}
	
	public void increaseSuspicion(float val = 1.0f){
		suspicionLevel += val;
		
		slider.sliderValue = suspicionLevel/100.0f;
		
		// Filled the bar
		if (slider.sliderValue >= 1.0f){
			Debug.Log("You got caught!!!");
		}
	}
	
	public void reduceSuspicion(float val = 1.0f){
		suspicionLevel -= val;
		Debug.Log ("Current");
		Debug.Log (suspicionLevel);
		if(suspicionLevel < 0){
			suspicionLevel = 0;
		}
		
		slider.sliderValue = suspicionLevel/100.0f;
	}

	public void collectAnswer()
	{
		answersCollected++;
		answersCollectedText.text = answersCollected + "\nof\n" + answersTotal;
	}

	public void endLevel()
	{
		// calculate score
		// fade to black and display score
		// reload level... for now
	}
}
