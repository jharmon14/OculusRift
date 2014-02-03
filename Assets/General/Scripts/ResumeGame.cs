using UnityEngine;
using System.Collections;

public class ResumeGame : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject Manager;
	// Use this for initialization
	void Start () {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        Manager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        Manager.GetComponent<GameManager>().TogglePause();
        NGUITools.SetActive(pauseMenu, false);
    }
}
