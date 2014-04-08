using UnityEngine;
using System.Collections;

public class ManagerCreator : MonoBehaviour {

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("GameManager") == null)
        {
            GameObject GameManager = Instantiate(gameManager) as GameObject;
            GameManager.name = "GameManager";
        }
	}
}
