using UnityEngine;
using System.Collections;

public class BulletRotation : MonoBehaviour {

    public bool direction;
    public PlayerMovement playerMovement;
	// Use this for initialization
	void Start () {
        playerMovement = GameObject.Find("PlayerParent").GetComponent<PlayerMovement>();
        direction = playerMovement.direction;
	}
	
	// Update is called once per frame
	void Update () {
        if (direction)
        {
            transform.Rotate(new Vector3(0, 3, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, -3, 0));
        }
	}
}
