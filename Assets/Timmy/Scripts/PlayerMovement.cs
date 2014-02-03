using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public bool paused;
    public GameObject Manager;

    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        paused = Manager.GetComponent<GameManager>().paused;
        if (paused)
            return;

        if (Input.GetKey("d"))
        {
            transform.Rotate(new Vector3(0, 1, 0), 1);
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0, 1, 0), -1);
        }

    }
}
