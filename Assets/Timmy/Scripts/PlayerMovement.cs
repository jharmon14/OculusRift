using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public bool paused;
    public GameObject Manager;
    public bool direction = true;
    public bool moving = false;
    public bool touchingGround;
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
        touchingGround = player.GetComponent<PlayerScript>().touchingGround;
        paused = Manager.GetComponent<GameManager>().paused;
        if (paused)
            return;

        if (Input.GetKey("d"))
        {
            moving = true;
            if(touchingGround)
                player.animation.Play("run");
            if (!direction)
            {
                direction = !direction;
                player.transform.Rotate(0, 180, 0);
            }
            transform.Rotate(new Vector3(0, 1, 0), 1);
        }
        else if (Input.GetKey("a"))
        {
            moving = true;
            if (touchingGround)
                player.animation.Play("run");
            if (direction)
            {
                direction = !direction;
                player.transform.Rotate(0, 180, 0);
            }
            transform.Rotate(new Vector3(0, 1, 0), -1);
        }
        else
        {
            moving = false;
        }

    }
}
