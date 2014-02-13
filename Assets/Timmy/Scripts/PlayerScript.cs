using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float jumpSpeed = 8.0f;
    private Vector3 jumpVelocity = new Vector3(0, 7, 0);
    public float gravity = 20.0F;
    public GameObject bulletClockWise;
    public GameObject bulletCounterClockWise;
    public PlayerMovement playerMovement;
    public bool direction;
    public bool moving;
    public bool touchingGround = true;

    private int health = 100;


    // Use this for initialization
    void Start()
    {
        playerMovement = GameObject.Find("PlayerParent").GetComponent<PlayerMovement>();
        bulletClockWise.SetActive(false);
        bulletCounterClockWise.SetActive(false);
    }

    void OnLevelWasLoaded(int level)
    {
        playerMovement = GameObject.Find("PlayerParent").GetComponent<PlayerMovement>();
        bulletClockWise.SetActive(false);
        bulletCounterClockWise.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moving = playerMovement.moving;
        direction = playerMovement.direction;
        if (touchingGround && !moving)
        {
            animation.Play("idle");
        }

        if (health <= 0)
            Debug.Log("Dead");

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
            bulletClockWise.SetActive(true);
            clone = Instantiate(bulletClockWise, new Vector3(bulletClockWise.transform.position.x, transform.position.y -.2f, bulletClockWise.transform.position.z), transform.rotation) as GameObject;
            bulletClockWise.SetActive(false);
        }
        else
        {
            bulletCounterClockWise.SetActive(true);
            clone = Instantiate(bulletCounterClockWise, new Vector3(bulletCounterClockWise.transform.position.x, transform.position.y - .2f, bulletCounterClockWise.transform.position.z), transform.rotation) as GameObject;
            bulletCounterClockWise.SetActive(false);
        }
        Destroy(clone, 1);
    }

    public void GotShot()
    {
        health -= 10;
    }
}
