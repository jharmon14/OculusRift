﻿using UnityEngine;
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

    private int health = 100;
    private GameObject pauseManager;
    private PauseManagerScript managerScript;
    private bool paused;
    private GameObject player;
    private bool move = false; //used to keep enemy moving beyond activeDistance and until maxDistance
    private GameObject bullets;
    private float lastFireTime;
    
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        target = player.transform;
        bullets = GameObject.Find("Bullets");
        bullets.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 heading = target.position - transform.position;
		dirNum = AngleDir(new Vector3(1, 0, 0), heading, transform.up);
        if (health <= 0)
        {
            Destroy(transform.gameObject);
        }
        if (Vector3.Angle(transform.position, player.transform.position) < activeDistance || move)
        {
            Debug.Log(Vector3.Angle(transform.position, player.transform.position));
            if (Vector3.Angle(transform.position, player.transform.position) < shootingDistance)
            {
                if (dirNum < 0)
                {
                    playerDirection = false;
                }
                else
                {
                    playerDirection = true;
                }
                if (Time.time > lastFireTime + fireRate)
                {
                    Fire();
                    lastFireTime = Time.time;
                }
            }
            else
            {
                animation.Play("walk");
                move = true;
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
        if (Vector3.Angle(transform.position, player.transform.position) > maxDistance)
        {
            move = false;
        }
	}

    public void GotShot()
    {
        health -= 50;
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