/************************************************************************************
 *
 * Filename :   GameManager.cs
 * Content  :   General level scripting, score tracking, game start and end scripting
 * Expects  :   Attached to dedicated GameManager object
 * Authors  :   Devin Turner
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// Constants
	public enum Levels
	{
		None = -1,
		Overworld = 0,
		FPS,
    Num_Levels
	}

	// Inspector variables
	public int[] score;
	public GameObject[] initialLevelManagers;
    public bool paused = false;
    public GameObject pauseMenu;
	public GameObject targetParent;

	// Hidden public variables
	[HideInInspector]
	public FPSManager fpsManager;

	[HideInInspector]
	public OverworldManager overworldManager;
	
	public int gmScoreFPS = 0;

	// Private variables
	private GameObject levelManagerGO;


	void Start()
	{
        pauseMenu.SetActive(false);
		levelManagerGO = Instantiate(initialLevelManagers[(int)Levels.Overworld]) as GameObject;
		overworldManager = levelManagerGO.GetComponent<OverworldManager>();
		score = new int[(int)Levels.Num_Levels];
		
		// Mouse Settings
		Screen.lockCursor = true;
	}

    void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenu");
    }

    void OnLevelWasLoaded(int level)
    {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
	}

	public void LoadLevel(Levels level)
	{
		DontDestroyOnLoad(this.gameObject);
		Destroy(levelManagerGO);
		int levelIndex;
		switch (level)
		{
			case Levels.FPS:
				levelIndex = (int)Levels.FPS;
				overworldManager = null;
				levelManagerGO = Instantiate(initialLevelManagers[levelIndex]) as GameObject;
				fpsManager = levelManagerGO.GetComponent<FPSManager>();
				break;
			case Levels.Overworld:
			default:
				levelIndex = (int)Levels.Overworld;
				fpsManager = null;
				levelManagerGO = Instantiate(initialLevelManagers[levelIndex]) as GameObject;
				overworldManager = levelManagerGO.GetComponent<OverworldManager>();
				break;
		}
		levelManagerGO.name = levelManagerGO.name.Replace("(Clone)", "");
		DontDestroyOnLoad(levelManagerGO);
		CameraFade.StartAlphaFade(Color.black, false, 2.0f, 2.0f, () => { Application.LoadLevel(levelIndex); });
		return;
	}

    public void TogglePause()
    {
        paused = !paused;
    }
}