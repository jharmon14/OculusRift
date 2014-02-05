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
	public GameObject bulletPrefab;
	
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
    }
   
    // Update is called once per frame
    void Update () 
	{		
		// don't update the enemies unless they are on screen, temporary arbitrary number of 50
		if(distance < 50)
		{
			// rotate the parent, calculate distance from player to enemy
			transform.parent.Rotate(Vector3.up * Time.deltaTime * moveSpeed);
			distance = Vector3.Distance (transform.position, target.transform.position);
			
			// if it's going to go out of bounds, reverse the direction
	        if (Quaternion.Angle(transform.parent.rotation, endRotation) > bound)
	            moveSpeed = -moveSpeed;
			
			// look at the player if it's close enough
			if(distance < 10)
				transform.LookAt(target.transform);
			
			// fire 
			if(Time.time > nextFire)// && distance < 20)
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
			
			// rotate the bullet's parent and check for collision
			if(bulletParent != null)
				bulletParent.transform.Rotate(Vector3.up * Time.deltaTime * Mathf.Abs(moveSpeed * 3));
		}
    }
}