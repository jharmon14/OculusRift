using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

    public float jumpSpeed = 8.0f;
    private Vector3 jumpVelocity = new Vector3(0, 7, 0);
    public float gravity = 20.0F;
    public GameObject bullet;

    private bool touchingGround = true;
    private int health = 100;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Debug.Log("Dead");

        if (touchingGround && Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingGround = false;
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
        clone = Instantiate(bullet, new Vector3(0, transform.position.y - 0.1f, 0), transform.rotation) as GameObject;
        Destroy(clone, 3);
    }

    public void GotShot()
    {
        health -= 10;
    }
}
