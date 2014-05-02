using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    public GameObject Manager;

    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        Manager.GetComponent<GameManager>().LoadLevel(GameManager.Levels.Overworld);
    }
}
