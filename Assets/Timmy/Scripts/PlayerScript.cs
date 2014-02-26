using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Vector3 jumpVelocity = new Vector3(0, 7, 0);
    public float gravity = 20.0F;
    public GameObject playerParent;
    public PlayerMovement playerMovement;
    public bool direction;
    public bool moving;
    public bool touchingGround = true;

    private int health = 100;
    private GameObject pauseManager;
    private PauseManagerScript managerScript;
    private bool paused;

    public GameObject bullets;

    // Use this for initialization
    void Start()
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        bullets = GameObject.Find("Bullets");
        bullets.SetActive(false);
    }

    void OnLevelWasLoaded(int level)
    {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        bullets = GameObject.Find("Bullets");
        bullets.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        paused = managerScript.paused;
        if (paused)
            return;

        moving = playerMovement.moving;
        direction = playerMovement.direction;
        if (touchingGround && !moving)
        {
            animation.Play("idle");
        }

        if (health <= 0)
            Destroy(transform.gameObject);

        if (touchingGround && Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingGround = false;
            animation.Play("jump");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void OnCollisionEnter()
    {
        touchingGround = true;
    }

    void OnCollisionExit()
    {
        touchingGround = false;
    }

    void Fire()
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
    }
}
