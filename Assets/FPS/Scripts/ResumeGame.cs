using UnityEngine;
using System.Collections;

public class ResumeGame : MonoBehaviour {

    public GameObject pauseMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        NGUITools.SetActive(pauseMenu, false);
        GameObject.Find("Ground").GetComponent<GameManager>().TogglePause();
       // isPaused = false;
        Time.timeScale = 1.0f;
    }
}
