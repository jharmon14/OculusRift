using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int activeDistance = 50; //degrees, angle at which enemy starts moving
    public int maxDistance = 85; //degrees, angle at which enemy stops moving
    public int maxDegree = 30; //degrees, angle enemy moves around cicle
    public int shootingDistance = 30; //degrees, angle at which enemy stops moving and starts shooting
    public int currentDegree = 0; //degrees, how far enemy has moved so far
    public bool direction = false; //direction enemy is moving, true = clockwise
    public float fireRate = 0.5f; //seconds, how long between enemy shots
    public bool playerDirection; //which way to shoot bullets
    public Transform target; //used for AngleDir function
	public float dirNum; //set by AngleDir function
    public bool shooter = false; //true if enemy shoots, false if enemy only patrols
    public GameObject bullets;

    private int health = 100;
    public GameObject manager;
    private GameManager managerScript;
    private bool paused;
    private GameObject player;
    private bool move = false; //used to keep enemy moving beyond activeDistance and until maxDistance
    private float lastFireTime;
    private bool pd = false;
    private bool flippedToShoot = false;
    private bool shooting = false;

    private TimmyManager tm;
    
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        target = player.transform;
        manager = GameObject.Find("GameManager");
        managerScript = manager.GetComponent<GameManager>();
        tm = GameObject.Find("TimmyManager").GetComponent<TimmyManager>();
	}
/*
    void Awake()
    {
        player = GameObject.Find("Player");
        target = player.transform;
        manager = GameObject.Find("GameManager");
        managerScript = manager.GetComponent<GameManager>();
    }

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.Find("Player");
        target = player.transform;
        bullets = GameObject.Find("EnemyBullets");
        //bullets.SetActive(false);
    }
	*/
	// Update is called once per frame
	void Update () {
        paused = managerScript.paused;
        if (paused)
        {
            return;
        }
        PlayerMovement playerMovement = GameObject.Find("PlayerParent").GetComponent<PlayerMovement>();

        
        if (!shooting)
        {
            pd = playerMovement.direction;
        }

        Vector3 heading = target.position - transform.position;
        if (pd)
        {
            dirNum = AngleDir(new Vector3(1, 0, 0), heading, transform.up);
        }
        else
        {
            dirNum = AngleDir(new Vector3(-1, 0, 0), heading, transform.up);
        }

        if (health <= 0)
        {
            tm.kills += 1;
            Destroy(transform.gameObject);
        }

        if (Vector3.Angle(transform.position, player.transform.position) < activeDistance || move)
        {
            if (Vector3.Angle(transform.position, player.transform.position) < shootingDistance && shooter)
            {
                shooting = true;
                if (dirNum < 0 && pd)
                {
                    playerDirection = false;
                }
                else if (dirNum < 0 && !pd) 
                {
                    playerDirection = true;
                }
                else if (dirNum > 0 && !pd)
                {
                    playerDirection = true;
                }
                else
                {
                    playerDirection = false;
                }
                if (Time.time > lastFireTime + fireRate)
                {
                    if (direction != playerDirection && !flippedToShoot)
                    {
                        transform.Rotate(0, 180, 0);
                        flippedToShoot = true;
                    }
                    Fire();
                    lastFireTime = Time.time;
                }
            }
            else
            {
                shooting = false;
                if (flippedToShoot)
                {
                    if (transform.name == "timmy_bat")
                    {
                        transform.Rotate(0, 0, 180);
                        flippedToShoot = false;
                    }
                    else
                    {
                        transform.Rotate(0, 180, 0);
                        flippedToShoot = false;
                    }
                }
                animation.Play();
                move = true;
                if (currentDegree == 0 || currentDegree == maxDegree)
                {
                    direction = !direction;
                    if (transform.name == "timmy_bat")
                    {
                        transform.Rotate(0, 0, 180);
                    }
                    else
                    {
                        transform.Rotate(0, 180, 0);
                    }
                }
                if (direction)
                {
                    transform.RotateAround(Vector3.zero, Vector3.up, .5f);
                    currentDegree += 1;
                }
                else
                {
                    transform.RotateAround(Vector3.zero, Vector3.up, -.5f);
                    currentDegree -= 1;
                }
            }
        }
        if (Vector3.Angle(transform.position, player.transform.position) > maxDistance)
        {
            move = false;
        }
	}
	
	public void moveEnemy(){
        if (managerScript.paused)
            return;
		// If the player is in enemy range, allow Update() to determine enemy movement
		if(Vector3.Angle(transform.position, player.transform.position) > maxDistance || 
            (Vector3.Angle(transform.position, player.transform.position) > activeDistance && !move))
        {
            shooting = false;
            if (flippedToShoot)
            {
                transform.Rotate(0, 180, 0);
                flippedToShoot = false;
            }
			
            animation.Play();
			
		    if (currentDegree == 0 || currentDegree == maxDegree)
		    {
		        direction = !direction;
		        transform.Rotate(0, 180, 0);
		    }
			
		    if (direction)
		    {
		        transform.RotateAround(Vector3.zero, Vector3.up, .5f);
		        currentDegree += 1;
		    }
		    else
		    {
		        transform.RotateAround(Vector3.zero, Vector3.up, -.5f);
		        currentDegree -= 1;
		    }
		}
	}

	void OnTriggerEnter(Collider collision) {
		// Move the Enemy
        if (paused)
        {
            return;
        }
        if(collision.gameObject.tag == "Light"){
			moveEnemy();
		}
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "PlayerBullet") 
        {
            GotShot();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Block")
        {
            if (direction)
            {
                maxDegree = currentDegree;
            }
            else
            {
                currentDegree = 0;
            }
        }
    }
	
	void OnTriggerStay(Collider collision){
        if (paused)
        {
            return;
        }
		if(collision.gameObject.tag == "Light"){
			moveEnemy();
		}
	}
	
	void OnTriggerExit(Collider collision){
    }
	
    public void GotShot()
    {
        if (managerScript.timmyCurrentLevel == 0)
        {
            health -= 50;
        }
        else
        {
            health -= 25;
        }
    }

    void Fire()
    {
        GameObject clone;
        if (direction)
        {
            bullets.SetActive(true);
            clone = Instantiate(bullets, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z),
                Quaternion.identity) as GameObject;
            Physics.IgnoreCollision(clone.collider, collider);
            clone.gameObject.tag = "EnemyBullet";
            clone.GetComponent<Bullet>().SetDirection(playerDirection);
            bullets.SetActive(false);
        }
        else
        {
            bullets.SetActive(true);
            clone = Instantiate(bullets, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z),
                Quaternion.identity) as GameObject;
            Physics.IgnoreCollision(clone.collider, collider);
            clone.gameObject.tag = "EnemyBullet";
            clone.GetComponent<Bullet>().SetDirection(playerDirection);
            bullets.SetActive(false);
        }
        Destroy(clone, 1);
    }

    /*
     * Found this function at http://forum.unity3d.com/threads/31420-Left-Right-test-function
     * Use this to determine which direction enemy needs to shoot in order to be aiming at player
     */
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
	}
	
}
