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
	public Transform atlasPrefab;

  [HideInInspector]
	public MisconductPlayerController player;
	[HideInInspector]
	public MisconductStudent playerStudent;
	[HideInInspector]
	public int startMins;
	[HideInInspector]
	public int startSecs;
	
	// Private variables
	private UISlider slider;
	private int answersCollected = 0;
	private int answersTotal = 0;
	private float suspicionLevel = 0;
	private UILabel answersCollectedText;
	private bool levelEnded = false;
	private bool won = false;
	private float score = 0;
	private GameManager gm;
	private float loadTime = 0;
	private bool loadingLevel = false;

	private UIAtlas atlas;
	private GameObject winText = null;
	private GameObject winGrade = null;
	private GameObject winScore = null;
	private GameObject loseText = null;
	private UISprite gradeSprite = null;


	void Start()
	{
		atlas = atlasPrefab.GetComponent<UIAtlas>();
	}

	// Update is called once per frame
	void Update()
	{
		// find win gui objects now that player has definitely been created
		if ((winText == null) || (winGrade == null) || (winScore == null) || (loseText == null))
		{
			winText = GameObject.Find("WinText");
			winGrade = GameObject.Find("WinGrade");
			winScore = GameObject.Find("WinScore");
			loseText = GameObject.Find("LoseText");
			winText.SetActive(false);
			winGrade.SetActive(false);
			winScore.SetActive(false);
			loseText.SetActive(false);
		}

		if ((answersCollected == answersTotal) && !levelEnded)
		{
			levelEnded = true;
			gameOver();
		}

		if ((gradeSprite != null) && (gradeSprite.fillAmount < 1))
		{
			gradeSprite.fillAmount += 1f * Time.deltaTime;
		}

		if ((loadTime > 0) && (Time.time > loadTime) && !loadingLevel)
		{
			loadingLevel = true;
			gm.LoadLevel(GameManager.Levels.Overworld);
		}
	}
	
	void Awake(){
		GameObject[] papers = GameObject.FindGameObjectsWithTag("Paper");
		answersTotal = papers.Length - 1; // minus one for the player's paper
		answersCollectedText = transform.FindChild("PapersCollected").GetComponent<UILabel>();
		answersCollectedText.text = answersCollected + "\nof\n" + answersTotal;
		slider = GameObject.Find("SuspicionProgressBar").GetComponent<UISlider>();
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void increaseSuspicion(float val = 1.0f){
		suspicionLevel += val;
		
		slider.sliderValue = suspicionLevel/100.0f;
		
		// Filled the bar
		if (slider.sliderValue >= 1.0f){
			Debug.Log("You got caught!!!");
			if (!levelEnded)
			{
				levelEnded = true;
				gameOver();
			}
		}
	}
	
	public void reduceSuspicion(float val = 1.0f){
		suspicionLevel -= val;
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

	public void gameOver()
	{
		Debug.Log(this.GetInstanceID());
		// calculate score
		score = calcScore();

		GameObject.Find("Teacher").GetComponent<MisconductTeacherMovement>().enabled = false;
		GameObject.Find("inverse_cone").SetActive(false);
		GameObject.Find("Player").GetComponent<MisconductPlayerController>().enabled = false;
		if (GameObject.Find("Progress Bar") != null)
		{
			GameObject.Find("Progress Bar").SetActive(false);
		}

		// Player caught cheating
		if (!won)
		{
			string loseStr = loseText.GetComponent<UILabel>().text;
			loseStr = loseStr.Replace("[insert]", "You've committed\nACADEMIC\nMISCONDUCT!");
			loseText.GetComponent<UILabel>().text = loseStr;
			loseText.SetActive(true);
		}
		else
		{
			winText.SetActive(true);
			float grade = (float)answersCollected / (float)answersTotal;
			if (grade < 0.6) // F
			{
				winGrade.GetComponent<UISprite>().spriteName = atlas.spriteList[6].name;
			}
			else if ((grade >= 0.6) && (grade < 0.7)) // D
			{
				winGrade.GetComponent<UISprite>().spriteName = atlas.spriteList[9].name;
			}
			else if ((grade >= 0.7) && (grade < 0.8)) // C
			{
				winGrade.GetComponent<UISprite>().spriteName = atlas.spriteList[8].name;
			}
			else if ((grade >= 0.8) && (grade < 0.9)) // B
			{
				winGrade.GetComponent<UISprite>().spriteName = atlas.spriteList[5].name;
			}
			else // A
			{
				winGrade.GetComponent<UISprite>().spriteName = atlas.spriteList[7].name;
			}
			gradeSprite = winGrade.GetComponent<UISprite>();
			winGrade.SetActive(true);
			winScore.GetComponent<UILabel>().text = score.ToString();
			winScore.SetActive(true);
		}
		gm.score[(int)GameManager.Levels.Misconduct] = (int)score;
		loadTime = Time.time + 5f;
		// fade to black and display score
		// reload level... for now
	}

	public float calcScore()
	{
		ClockCountdown cc = GameObject.Find("Clock").GetComponent<ClockCountdown>();
		// lose condition
		if (slider.sliderValue >= 1f)
		{
			return 0;
		}
		float initScore = ((float)answersCollected / (float)answersTotal) * scoreMultiplier;
		float score = 0;
		score = initScore - (initScore * 0.5f * (slider.sliderValue));
		float totalSecs = (startMins * 60f) + startSecs;
		float endSecs = (cc.minutes * 60f) + cc.seconds;
		score += (initScore * 0.5f * (endSecs/totalSecs));
		Debug.Log("Final score: " + score);
		won = true;
		return Mathf.Round(score);
	}
}
