using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour 
{
	void OnTriggerEnter(Collider collision)
	{
		if(collision.name == "Player")
		{
			//Destroy(collision.collider.gameObject);
			//Destroy(gameObject);
			Debug.Log("Hit");
		}
	}	
}
