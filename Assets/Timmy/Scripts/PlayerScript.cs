using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //public Vector3 jumpVelocity = new Vector3(0, 7, 0);
   // public float gravity = 20.0F;
    public GameObject playerParent;
    public PlayerMovement playerMovement;
    public bool direction;
    public int damageAmount;
	
	// Collision variables
    public bool touchingGround = true;
	public bool touchingBlock = false;
	public bool touchingEnemy = false;

    public int health = 100;
    public GameObject bullets;

    public UISlider healthBar;

    private GameObject manager;
    private GameManager managerScript;
    private bool paused;
    private UILabel gameTime;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");
        managerScript = manager.GetComponent<GameManager>();
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        bullets = GameObject.Find("Bullets");
        bullets.SetActive(false);
        healthBar = GameObject.Find("HealthBar").GetComponent<UISlider>();
        gameTime = GameObject.Find("GameTime").GetComponent<UILabel>();
    }
    /*
    void OnLevelWasLoaded(int level)
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        bullets = GameObject.Find("Bullets");
        //bullets.SetActive(false);
    }
    */
    // Update is called once per frame

    void Update()
    {
    	direction = playerMovement.direction;
        paused = managerScript.paused;
        gameTime.text = Mathf.RoundToInt(Time.time).ToString();

    }

    void OnCollisionEnter(Collision collision)
    {
		/*if(collision.gameObject.tag == "Block"){
			touchingBlock = true;
		} else */
		if (collision.gameObject.tag == "Enemy"){
			touchingEnemy = true;
		} else {
        	touchingGround = true;
		}
    }
	
    void OnCollisionExit(Collision collision)
    {
		/*if(collision.gameObject.tag == "Block"){
			touchingBlock = false;
		} else */
		if (collision.gameObject.tag == "Enemy"){
			touchingEnemy = false;
		} else {
        	touchingGround = false;
		}
    }
	
	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Block"){
			touchingBlock = true;
		}
	}
	
	void OnTriggerExit(Collider collision){
		if(collision.gameObject.tag == "Block"){
			touchingBlock = false;
		}
	}

    public void Fire()
    {
        GameObject clone;
        if (direction)
        {
            bullets.SetActive(true);
            clone = Instantiate(bullets, new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z),
                Quaternion.identity) as GameObject;
            Physics.IgnoreCollision(clone.collider, collider);
            clone.gameObject.tag = "PlayerBullet";
            bullets.SetActive(false);
        }
        else
        {
            bullets.SetActive(true);
            clone = Instantiate(bullets, new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z),
                Quaternion.identity) as GameObject;
            Physics.IgnoreCollision(clone.collider, collider);
            clone.gameObject.tag = "PlayerBullet";
            bullets.SetActive(false);
        }
        Destroy(clone, 1);
    }

    public void GotShot()
    {
        health -= 10;
        healthBar.sliderValue = health/100.0f;
		//Debug.Log ("Health: " + health.ToString());
    }

    public void DieAndRespawn()
    {
        if (managerScript.timmyLives > 1)
        {
            managerScript.timmyLives--;
            Destroy(gameObject);
            managerScript.paused = true;
            int finalScore = 1;
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.score[(int)GameManager.Levels.Timmy] += (int)finalScore;
            gm.LoadLevel(GameManager.Levels.Timmy);
        }
        else
        {
            managerScript.timmyLives = 3;
            Destroy(gameObject);
            managerScript.paused = true;
            int finalScore = 1;
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.score[(int)GameManager.Levels.Timmy] += (int)finalScore;
            gm.LoadLevel(GameManager.Levels.Overworld);
        }
    }
}
