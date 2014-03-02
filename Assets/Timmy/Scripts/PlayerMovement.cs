using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public bool paused;
    public GameObject Manager;
	// True is clockwise; False is counterclockwise
    public bool direction = true;
    public bool moving = false;
    public bool touchingGround;
	public bool touchingBlock;
	public float radius = 10;
	
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
		touchingBlock = player.GetComponent<PlayerScript>().touchingBlock;
        paused = Manager.GetComponent<GameManager>().paused;
		
        if (paused)
            return;

        if (Input.GetKey("d"))
        {
			if(!touchingBlock){
	            moving = true;
				if (touchingGround)
	                player.animation.Play("run");
	            if (!direction)
	            {
	                direction = !direction;
	                player.transform.Rotate(0, 180, 0);
	            }
	            transform.Rotate(new Vector3(0, 1, 0), 1);
			} else if(!direction) {
	            moving = true;
	            if(touchingGround && !touchingBlock)
	                player.animation.Play("run");
	            if (!direction)
	            {
	                direction = !direction;
	                player.transform.Rotate(0, 180, 0);
	            }
	            transform.Rotate(new Vector3(0, 1, 0), 1);
			}
        }
        else if (Input.GetKey("a"))
        {
			if(!touchingBlock){
	            moving = true;
				if (touchingGround)
	                player.animation.Play("run");
	            if (direction)
	            {
	                direction = !direction;
	                player.transform.Rotate(0, 180, 0);
	            }
	            transform.Rotate(new Vector3(0, 1, 0), -1);
			} else if(direction) {
	            moving = true;

	            if (direction)
	            {
	                direction = !direction;
	                player.transform.Rotate(0, 180, 0);
	            }
	            transform.Rotate(new Vector3(0, 1, 0), -1);
			}
        }
        else
        {
            moving = false;
        }

    }
}
