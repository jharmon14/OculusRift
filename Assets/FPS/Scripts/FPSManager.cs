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
	public int civiliansHit = 0;
    [HideInInspector]
    public int shotsFired = 0;
    [HideInInspector]
    public float accuracy;
    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public float timeStarted = 0.0f;
    [HideInInspector]
    public float timeEnded = 0.0f;

    void Awake()
    {
        // Fade in the camera
        CameraFade.StartAlphaFade(Color.black, true, 4.0f);
		Screen.showCursor = false;
    }

    public void Update()
    {
		Screen.showCursor = false;
        accuracy = shotsFired > 0 ? (float)targetsHit / (float)shotsFired : 1;
        score = (int)(((targetsHit * targetScoreMultiplier) * (accuracy * accuracyScoreMultiplier)) / ((timeEnded - timeStarted) / 60) - (civiliansHit * 100));

		// in case you shoot too many civilians or are just awful...or something
		if(score < 0)
			score = 0;
    }

    public void LevelEnd(float time)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.score[(int)GameManager.Levels.FPS] = (int)this.score;
        gm.LoadLevel(GameManager.Levels.Overworld);
    }
}