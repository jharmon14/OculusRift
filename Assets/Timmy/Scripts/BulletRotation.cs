using UnityEngine;
using System.Collections;

public class BulletRotation : MonoBehaviour {

    public bool direction;
    public PlayerMovement playerMovement;
    public GameObject playerParent;
	// Use this for initialization
	void Start () {
        playerParent = GameObject.Find("PlayerParent");
        playerMovement = playerParent.GetComponent<PlayerMovement>();
        direction = playerMovement.direction;
        transform.rotation = playerParent.transform.rotation;
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
