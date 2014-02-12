/************************************************************************************
 *
 * Filename :   FPSManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   (Probably) an OVRPlayerController, tagged enemies
 * Authors  :   Devin Turder hahaha
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class FPSManager : MonoBehaviour
{
	public int targetsHit = 0;
	public int shotsFired = 0;
	public int targetScoreMultiplier = 100;
	public int accuracyScoreMultiplier = 10;
	public float timeStarted = 0.0f;

	void Awake()
	{
		// Fade in the camera
		CameraFade.StartAlphaFade(Color.black, true, 4.0f);
	}

	public void LevelEnd(float time)
	{
    // tally up the score and report to game manager
		float accuracy = targetsHit / shotsFired;
		float finalScore = ((targetsHit * targetScoreMultiplier) / (accuracy * accuracyScoreMultiplier)) / (time - timeStarted);
		GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		gm.score[(int)GameManager.Levels.FPS] = (int)finalScore;
		gm.LoadLevel(GameManager.Levels.Overworld);
	}
}