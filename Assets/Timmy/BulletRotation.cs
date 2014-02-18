using UnityEngine;
using System.Collections;

public class BulletRotation : MonoBehaviour 
{

    public bool direction = true;
    public EnemyController enemyController;
	// Use this for initialization
	void Start () 
	{
        //enemyController = GameObject.Find("PlayerParent").GetComponent<EnemyController>();
		//enemyController = transform.parent.gameObject.GetComponent<EnemyController>();
        //direction = enemyController.direction;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (direction)
            transform.Rotate(new Vector3(0, 1.5f, 0));
		
        else
            transform.Rotate(new Vector3(0, -1.5f, 0));
	}
}
