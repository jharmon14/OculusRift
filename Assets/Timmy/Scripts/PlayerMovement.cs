using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public bool paused;
    public GameObject Manager;
	public PlayerScript playerScript;

	// Movement Variables
	public Vector3 jumpVelocity = new Vector3(0, 7, 0);
	public Vector3 wallJumpVelocity = new Vector3(0, 9, 0);
	public int knockBack = 0;
    public float gravity = 20.0F;
    public bool direction = true;
	public bool lockDirection = false;
	public bool moving = false;
	
	// Player Variables
	private int health = 100;
	
	
    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
		playerScript = player.GetComponent<PlayerScript>();
    }

    void OnLevelWasLoaded(int level)
    {
        Manager = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
		playerScript = player.GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
    void Update()
    {		
		// Pausing the game
        paused = Manager.GetComponent<GameManager>().paused;
        if (paused)
            return;
		
		// Essentially an easier implementation of slerp
		if (knockBack != 0){
			if(direction)
				transform.Rotate(0, -1, 0);
			else
				transform.Rotate(0, 1, 0);
			knockBack--;
			return;
		}
		
		// If the player dies
		if (playerScript.health <= 0)
            playerScript.DieAndRespawn();
			//return;
		
		
		// Independent Inputs
        if (Input.GetButtonDown("Fire1"))
        {
            playerScript.Fire();
        }
		
		
		// Overlapping Inputs
		if (playerScript.touchingGround && Input.GetButtonDown("Jump"))
		{//Player is on the ground, and wants to jump;
			player.rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            playerScript.touchingGround = false;
            player.animation.Play("jump");
			
		}
		else if (playerScript.touchingBlock)
		{// Touching Side of block
			
			if(playerScript.touchingGround)
			{// Movement Option
				if(Input.GetKey("d") && !direction)
				{
					moving = true;
					
	
		            direction = !direction;
	                player.transform.Rotate(0, 180, 0);
	
		            transform.Rotate(0, 1, 0);
	
		        }
		        else if (Input.GetKey("a") && direction)
				{
	
					moving = true;
	
		           	direction = !direction;
	                player.transform.Rotate(0, 180, 0);
	            	transform.Rotate(0, -1, 0);
	
		        }
				else if (Input.GetKey("a") || Input.GetKey("d"))
				{
		        	player.animation.Play("run");
				}
				else{
					moving = false;
					player.animation.Play("idle");
				}
			} 
			else { // Jumping so Wall Jump
				if(Input.GetButtonDown("Jump"))
				{
					// Switch Direction
					player.transform.Rotate(0, 180, 0);
					if(direction)
	            		transform.Rotate(0, -2, 0);
					else
						transform.Rotate(0, 2, 0);
					direction = !direction;
					
					// 'Bounce' Off Wall
					player.rigidbody.AddForce(wallJumpVelocity, ForceMode.VelocityChange);
		            player.animation.Play("jump");
				}
			}
			
		}
		else if (playerScript.touchingEnemy)
		{// Take damage and bounce back

			playerScript.GotShot();
			knockBack = 10;
			
			
		}
		else if(Input.GetKey("d"))
        {
            moving = true;
            if(playerScript.touchingGround)
                player.animation.Play("run");
            if (!direction)
            {
                direction = !direction;
                player.transform.Rotate(0, 180, 0);
            }
            transform.Rotate(0, 1, 0);
        }
        else if (Input.GetKey("a"))
        {
            moving = true;
            if (playerScript.touchingGround)
                player.animation.Play("run");
            if (direction)
            {
                direction = !direction;
                player.transform.Rotate(0, 180, 0);
            }
            transform.Rotate(0, -1, 0);
        }
		else if(!moving)
        {// Just standing there
            player.animation.Play("idle");
        }
		else
		{
			moving = false;
			
		}
    }
	
}
