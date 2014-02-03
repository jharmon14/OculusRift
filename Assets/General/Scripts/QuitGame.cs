using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    public GameObject Manager;
    public GameObject pauseMenu;
    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        pauseMenu = GameObject.FindWithTag("PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        Manager.GetComponent<GameManager>().paused = false;
        Manager.GetComponent<GameManager>().LoadLevel(GameManager.Levels.Overworld);
    }
}
