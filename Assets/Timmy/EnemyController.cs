using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{

    private Quaternion startRotation, endRotation;
	private Vector3 targetPoint;
	private GameObject target;
	private float distance;
	private float nextFire;
	
    public float moveSpeed = 1.0f;
	public GameObject bulletPrefab;
	
	// use this to change how far it will rotate the game object on an enemy by enemy basis
	public float bound = 0;
	
	// how much the enemy shoots
	public float fireRate = 2;
	
    // Use this for initialization
    void Start () 
	{
        startRotation = transform.parent.rotation;
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
			transform.parent.Rotate(Vector3.up * Time.deltaTime * moveSpeed);
			distance = Vector3.Distance (transform.position, target.transform.position);
			
			// if it's going to go out of bounds, reverse the direction
	        if (Quaternion.Angle(transform.parent.rotation, endRotation) > bound)
	        {
	            moveSpeed = -moveSpeed;
	            var temp = startRotation;
	            startRotation = endRotation;
	            endRotation = temp;
	        }
			
			// look at the player if it's close enough
			if(distance < 10)
				transform.LookAt(target.transform);
			
			// fire 
			if(Time.time > nextFire)// && distance < 20)
			{
				nextFire += Time.time;
				GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
				//bullet.transform.parent = transform.parent;
				bullet.transform.rotation = Quaternion.Lerp(bullet.transform.rotation, endRotation, Time.time * moveSpeed * 2);
				
			}
		}
    }
}