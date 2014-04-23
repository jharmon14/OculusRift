using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public GameObject Manager;
    public GameManager managerScript;
	public GameObject camera;
	public bool paused;
	public PlayerScript playerScript;

	// Movement Variables
	public Vector3 jumpVelocity = new Vector3(0, 7, 0);
	public Vector3 wallJumpVelocity = new Vector3(0, 7, 0);
	
	public float cameraYPosition;
	
	public int knockBack = 0;
    public float gravity = 20.0F;
    public bool direction = true;
	public bool lockDirection = false;
	public bool moving = false;
	
	// Jumping variables for FixedUpdate
	private bool fireWallJump = false;
	private bool fireJump = false;
	
	// Player Variables
	private int health = 100;
	
	
    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("GameManager");
        managerScript = Manager.GetComponent<GameManager>();
        player = GameObject.Find("Player");
		playerScript = player.GetComponent<PlayerScript>();
		camera = GameObject.Find("OVRCameraController");
		cameraYPosition = camera.transform.position.y;
    }

    void OnLevelWasLoaded(int level)
    {
        Manager = GameObject.Find("GameManager");
        managerScript = Manager.GetComponent<GameManager>();
        player = GameObject.Find("Player");
		camera = GameObject.Find("OVRCameraController");
		playerScript = player.GetComponent<PlayerScript>();
		cameraYPosition = camera.transform.position.y;
    }
	
	void FixedUpdate()
	{
		if(fireJump){
			player.rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			
            player.animation.Play("jump");
			fireJump = false;
		}
		else if(fireWallJump){
			player.rigidbody.AddForce(wallJumpVelocity, ForceMode.VelocityChange);

            player.animation.Play("jump");
			
			fireWallJump = false;
		}
		
	}
	
	// Update is called once per frame
    void Update()
    {		
		// Pausing the game
        paused = managerScript.paused;
        if (paused)
            return;
		
		camera.transform.position = new Vector3(0, cameraYPosition + player.transform.position.y, 0);
		
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
			//player.rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			fireJump = true;
			
            playerScript.touchingGround = false;
            //player.animation.Play("jump");
			
		}
		else if (playerScript.touchingBlock)
		{// Touching Side of block
			
			if(playerScript.touchingGround)
			{// Movement Option
                if (Input.GetKey("d") && !direction)
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
					fireWallJump = true;
					// 'Bounce' Off Wall
					//player.rigidbody.AddForce(wallJumpVelocity, ForceMode.VelocityChange);
					
		            //player.animation.Play("jump");
				}
			}
			
		}
		else if (playerScript.touchingEnemy)
		{// Take damage and bounce back

			playerScript.GotShot();
			knockBack = 10;
			
			
		}
        else if (Input.GetKey("d"))
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
			if(playerScript.touchingGround){
        		player.animation.Play("idle");
			}
        }
		else
		{
			moving = false;
			
		}
    }
	
}
