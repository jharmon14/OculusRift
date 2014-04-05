/************************************************************************************
 *
 * Filename :   StartGame.cs
 * Content  :   Start arcade game the player is looking at
 * Expects  :   Arcade machine with "StartButton" tag and StartButton script
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Inspector variables
    public GameObject initialGameManager;
    public GameManager.Levels level = GameManager.Levels.None;
	public AudioClip coinSound;
	
	private bool hasStartedGame;
	
	// Private variables
	
	void Awake()
    {
        //if (GameObject.Find("GameManager") == null)
        //{
        //    GameObject gm = Instantiate(initialGameManager) as GameObject;
        //    gm.name = "GameManager";
        //}
		
		hasStartedGame = false;
	}
	
	// Update is called once per frame
	void Update()
    {
        // Player presses fire1 button
        if (Input.GetButtonDown("Fire1") && (level > GameManager.Levels.None))
        {
			if(!hasStartedGame)
			{
				audio.PlayOneShot(coinSound);
				hasStartedGame = true;
			}
			
            GameObject.Find("GameManager").GetComponent<GameManager>().LoadLevel(level);
        }
	}
}
