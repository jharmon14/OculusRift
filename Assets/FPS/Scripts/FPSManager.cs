/************************************************************************************
 *
 * Filename :   FPSManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   (Probably) an OVRPlayerController, tagged enemies
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class FPSManager : MonoBehaviour
{
	public int targetScoreMultiplier = 100;
	public int accuracyScoreMultiplier = 10;

    [HideInInspector]
	public int targetsHit = 0;
    [HideInInspector]
	public int shotsFired = 0;
    [HideInInspector]
    public float accuracy;
    [HideInInspector]
    public float score;
    [HideInInspector]
	public float timeStarted = 0.0f;
    [HideInInspector]
    public float timeEnded = 0.0f;

	void Awake()
	{
		// Fade in the camera
		CameraFade.StartAlphaFade(Color.black, true, 4.0f);
	}

    public void Update()
    {
		accuracy = shotsFired > 0 ? (float)targetsHit / (float)shotsFired : 1;
		score = ((targetsHit * targetScoreMultiplier) / (accuracy * accuracyScoreMultiplier)) / ((timeEnded - timeStarted) / 60);
    }

	public void LevelEnd(float time)
	{
		GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		gm.score[(int)GameManager.Levels.FPS] = (int)this.score;
		gm.LoadLevel(GameManager.Levels.Overworld);
	}
}