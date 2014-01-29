using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Onclick()
    {
        GameObject.Find("GameManage").GetComponent<GameManager>().LoadLevel(GameManager.Levels.Overworld);
    }
}
