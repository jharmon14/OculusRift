using UnityEngine;
using System.Collections;

public class PauseManagerScript : MonoBehaviour {

    public GameObject Manager;
    public bool paused;
	// Use this for initialization
	void Start () {
        Manager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
        paused = Manager.GetComponent<GameManager>().paused;
	}
}
