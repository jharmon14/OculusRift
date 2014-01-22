using UnityEngine;
using System.Collections;

public class ResumeGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        NGUITools.SetActive(pauseMenu, false);
        GameObject.Find("Plane").GetComponent<GameManager>().TogglePause();
       // isPaused = false;
        Time.timeScale = 1.0f;
    }
}
