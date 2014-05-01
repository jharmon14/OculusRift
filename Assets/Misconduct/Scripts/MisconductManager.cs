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
	public float scoreMultiplier = 10000f;

  [HideInInspector]
	public MisconductPlayerController player;
	[HideInInspector]
	public MisconductStudent playerStudent;
	[HideInInspector]
	public int startMins;
	[HideInInspector]
	public int startSecs;
	
	private UISlider slider;
	private int answersCollected = 0;
	private int answersTotal = 0;
	private float suspicionLevel = 0;
	private UILabel answersCollectedText;
	private bool levelEnded = false;


	// Private variables

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (answersCollected == answersTotal)
		{
			endLevel();
		}
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
			if (!levelEnded)
			{
				endLevel();
				levelEnded = true;
			}
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
		calcScore();
		// calculate score
		// fade to black and display score
		// reload level... for now
	}

	public float calcScore()
	{
		ClockCountdown cc = GameObject.Find("Clock").GetComponent<ClockCountdown>();
		// lose conditions
		if (((cc.seconds <= 0) && (cc.minutes <= 0)) ||
			(slider.sliderValue >= 1f))
		{
			Debug.Log("no points for losing.");
			// return score of 0
			return 0;
		}
		float initScore = ((float)answersCollected / (float)answersTotal) * scoreMultiplier;
		float score = 0;
		score = initScore - (initScore * 0.5f * (slider.sliderValue));
		float totalSecs = (startMins * 60f) + startSecs;
		float endSecs = (cc.minutes * 60f) + cc.seconds;
		score += (initScore * 0.5f * (endSecs/totalSecs));
		Debug.Log("Final score: " + score);
		return score;
	}
}
