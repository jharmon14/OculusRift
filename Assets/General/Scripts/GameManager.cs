/************************************************************************************
 *
 * Filename :   GameManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   (Probably) an OVRPlayerController, tagged enemies
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Inspector variables
	public int score;
	
	// Hidden public variables
	[HideInInspector]
	public FPSManager fpsManager;
	
	[HideInInspector]
	public OverworldManager overworldManager;
	
	[HideInInspector]
	public enum Levels { Overworld, FPS, Timmy, Misconduct };
	
	// Private variables
	
	
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LoadLevel(Levels level) {
		DontDestroyOnLoad(gameObject);
		switch (level) {
			case Levels.Overworld:
				Application.LoadLevel("Arcade");
				overworldManager = new OverworldManager();
				break;
			case Levels.FPS:
				Application.LoadLevel("FPS_Scene");
				fpsManager = new FPSManager();
				break;
		}
		return;
	}
}