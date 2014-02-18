using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{

    private Quaternion endRotation;
	private GameObject target;
	private float distance;
	private float nextFire;
	private GameObject bulletParent, bullet;
	
    public float moveSpeed = 1.0f;
	public bool direction = true;
	public GameObject bulletPrefab;
	public GameObject bulletClockWise;
    public GameObject bulletCounterClockWise;
	
	// use this to change how far it will rotate the game object on an enemy by enemy basis (higher = larger rotation
	public float bound = 0;
	
	// how much the enemy shoots (lower = faster)
	public float fireRate = 1;
	
    // Use this for initialization
    void Start () 
	{
        endRotation = transform.rotation;
		target = GameObject.Find("Player");
		nextFire = 0;
		bulletClockWise.SetActive(false);
        bulletCounterClockWise.SetActive(false);
    }
   
    // Update is called once per frame
    void Update () 
	{		
		// don't update the enemies unless they are on screen, temporary arbitrary number of 50
		if(distance < 50)
		{			
			// rotate the parent, calculate distance from player to enemy
			transform.parent.Rotate(new Vector3(0, 1, 0), Time.deltaTime * moveSpeed);
			distance = Vector3.Distance (transform.position, target.transform.position);
			
			// if it's going to go out of bounds, reverse the direction
	        if (Quaternion.Angle(transform.parent.rotation, endRotation) > bound)
			{
	            moveSpeed = -moveSpeed;
				transform.Rotate(0, 180, 0);
				direction = !direction;
			}
			
			// look at the player if it's close enough
			if(distance < 10)
				transform.LookAt(target.transform);
			
			/*
			if(Time.time > nextFire)// && distance < 20)
			{
				nextFire = Time.time + fireRate;
				Fire();
			}
			*/
			
			// always destroy the bullet, even if the enemy is out of range of the player
			// redunant but necessary
			if(Time.time > nextFire)
			{
				if(bullet != null)
				{
					Destroy(bullet);
					Destroy(bulletParent);
				}
			}
			
			// fire 
			if(Time.time > nextFire && distance < 20)
			{
				// destroy previous bullet
				if(bullet != null)
				{
					Destroy(bullet);
					Destroy(bulletParent);
				}
				
				nextFire = Time.time + fireRate;
				
				// create a bullet, make a temporary parent to that bullet and then rotate it
				// with double the enemy movement speed to show the movement of the bullet
				bulletParent = new GameObject();
				bulletParent.transform.rotation = transform.parent.rotation;
				bulletParent.transform.position = transform.parent.position;
				
				bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
				bullet.transform.parent = bulletParent.transform;				
			}
			
			// rotate the bullet's parent based on the direction its facing
			if(bulletParent != null && direction == true)
				bulletParent.transform.Rotate(Vector3.up * Time.deltaTime * Mathf.Abs(moveSpeed * 3));
			
			else if(bulletParent != null && direction == false)
				bulletParent.transform.Rotate(Vector3.up * Time.deltaTime * -(Mathf.Abs(moveSpeed * 3)));	
				
			
		}
	}
	
	/*
	void Fire()
    {
        GameObject clone;
        if (direction)
        {
            bulletClockWise.SetActive(true);
            //clone = Instantiate(bulletClockWise, 
                //new Vector3(bulletClockWise.transform.position.x, transform.position.y -.2f, bulletClockWise.transform.position.z), 
                //transform.rotation) as GameObject;
			clone = (GameObject)Instantiate(bulletClockWise, bulletClockWise.transform.position, bulletClockWise.transform.rotation);
            bulletClockWise.SetActive(false);
        }
        else
        {
            bulletCounterClockWise.SetActive(true);
            //clone = Instantiate(bulletCounterClockWise, 
                //new Vector3(bulletCounterClockWise.transform.position.x, transform.position.y - .2f, bulletCounterClockWise.transform.position.z), 
                //transform.rotation) as GameObject;
			clone = (GameObject)Instantiate(bulletCounterClockWise, bulletCounterClockWise.transform.position, bulletCounterClockWise.transform.rotation);
            bulletCounterClockWise.SetActive(false);
        }
        Destroy(clone, 1);
    }
    */
    
    
}

