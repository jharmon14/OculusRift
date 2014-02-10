using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public bool paused;
    public GameObject Manager;

    private bool direction = true;

    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
    }

    void OnLevelWasLoaded(int level)
    {
        Manager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        paused = Manager.GetComponent<GameManager>().paused;
        if (paused)
            return;

        if (Input.GetKey("d"))
        {
            if (!direction)
            {
                direction = !direction;
                player.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            transform.Rotate(new Vector3(0, 1, 0), 1);
        }
        if (Input.GetKey("a"))
        {
            if (direction)
            {
                direction = !direction;
                player.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            transform.Rotate(new Vector3(0, 1, 0), -1);
        }

    }
}
