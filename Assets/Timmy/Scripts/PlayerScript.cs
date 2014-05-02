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
    public UILabel gameTime;
    public UILabel livesLeft;

    private GameObject manager;
    private GameManager managerScript;
    private bool paused;
    private TimmyManager tm;
    private float time;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");
        managerScript = manager.GetComponent<GameManager>();
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        //bullets = GameObject.Find("Bullets");
        //bullets.SetActive(false);
        healthBar = GameObject.Find("HealthBar").GetComponent<UISlider>();
        gameTime = GameObject.Find("GameTime").GetComponent<UILabel>();
        livesLeft = GameObject.Find("LivesLeft").GetComponent<UILabel>();
        livesLeft.text = "Lives: " + managerScript.timmyLives;
        tm = GameObject.Find("TimmyManager").GetComponent<TimmyManager>();
        Screen.lockCursor = true;
    }
    
    void OnLevelWasLoaded(int level)
    {
        manager = GameObject.Find("GameManager");
        managerScript = manager.GetComponent<GameManager>();
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        //bullets = GameObject.Find("Bullets");
        //bullets.SetActive(false);
        healthBar = GameObject.Find("HealthBar").GetComponent<UISlider>();
        gameTime = GameObject.Find("GameTime").GetComponent<UILabel>();
        livesLeft = GameObject.Find("LivesLeft").GetComponent<UILabel>();
        livesLeft.text = "Lives: " + managerScript.timmyLives;
        tm = GameObject.Find("TimmyManager").GetComponent<TimmyManager>();
        Screen.lockCursor = true;
        time = 0;
    }
    
    // Update is called once per frame

    void Update()
    {
        if (managerScript.paused)
            return;
    	direction = playerMovement.direction;
        paused = managerScript.paused;
        time = time + Time.deltaTime;
        gameTime.text = Mathf.RoundToInt(time).ToString();
        if (transform.position.y < -10)
        {
            DieAndRespawn();
        }
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
		if(collision.gameObject.tag == "Block")
        {
			touchingBlock = true;
		}
        if (collision.gameObject.tag == "EnemyBullet")
        {
            GotShot();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "EndLevel")
        {
            NextLevel();
        }
	}
	
	void OnTriggerExit(Collider collision){
		if(collision.gameObject.tag == "Block")
        {
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
        if (managerScript.timmyCurrentLevel == 0)
        {
            health -= 10;
        }
        else
        {
            health -= 25;
        }
        healthBar.sliderValue = health/100.0f;
    }

    public void DieAndRespawn()
    {
        managerScript.paused = true;
        if (managerScript.timmyLives > 1)
        {
            managerScript.timmyLives--;
            Destroy(gameObject);
            int finalScore = CalculateScore(int.Parse(gameTime.text));
            managerScript.score[(int)GameManager.Levels.Timmy + managerScript.timmyCurrentLevel] += (int)finalScore;
            managerScript.LoadLevel(GameManager.Levels.Timmy + managerScript.timmyCurrentLevel);
        }
        else
        {
            managerScript.timmyLives = 3;
            Destroy(gameObject);            
            int finalScore = CalculateFinalScore(int.Parse(gameTime.text));
            managerScript.score[(int)GameManager.Levels.Timmy + managerScript.timmyCurrentLevel] += (int)finalScore;
            managerScript.timmyCurrentLevel = 0;
            managerScript.LoadLevel(GameManager.Levels.Overworld);
        }
    }

    public void NextLevel()
    {
        managerScript.timmyCurrentLevel += 1;
        
        if (managerScript.timmyCurrentLevel <= 2)
        {
            managerScript.LoadLevel(GameManager.Levels.Timmy + managerScript.timmyCurrentLevel);
        }
        else
        {
            managerScript.LoadLevel(GameManager.Levels.Overworld);
        }
    }

    public int CalculateScore(int stopTime)
    {
        int score = 0;
        int kills = tm.kills;
        int time = stopTime;
        int lives = managerScript.timmyLives;

        score = kills;

        return score;
    }

    public int CalculateFinalScore(int stopTime)
    {
        int score = 0;
        int kills = tm.kills;
        int time = stopTime;
        int lives = managerScript.timmyLives;
        if (managerScript.timmyCurrentLevel == 0)
        {
            if (lives == 0)
            {
                score = (kills * 100 / time) * 10;
            }
            else if (lives == 1)
            {
                score = (kills * 100 / time) * 50;
            }
            else if (lives == 2)
            {
                score = (kills * 100 / time) * 100;
            }
            else
            {
                score = (kills * 100 / time) * 150;
            }
        } 
        else if (managerScript.timmyCurrentLevel == 1)
        {
            if (lives == 0)
            {
                score = (kills * 100 / time) * 20;
            }
            else if (lives == 1)
            {
                score = (kills * 100 / time) * 60;
            }
            else if (lives == 2)
            {
                score = (kills * 100 / time) * 110;
            }
            else
            {
                score = (kills * 100 / time) * 160;
            }
        }
        else
        {
            if (lives == 0)
            {
                score = (kills * 100 / time) * 30;
            }
            else if (lives == 1)
            {
                score = (kills * 100 / time) * 70;
            }
            else if (lives == 2)
            {
                score = (kills * 100 / time) * 120;
            }
            else
            {
                score = (kills * 100 / time) * 170;
            }
        }
        return score;
    }
}
