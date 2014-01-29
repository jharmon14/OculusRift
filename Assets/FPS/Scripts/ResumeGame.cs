using UnityEngine;
using System.Collections;

public class ResumeGame : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject Manager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        NGUITools.SetActive(pauseMenu, false);
        Manager.GetComponent<GameManager>().TogglePause();
    }
}
