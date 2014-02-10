using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");
        }
        Destroy(transform.parent.gameObject);
        //For Enemy bullets
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript p = collision.gameObject.GetComponent<PlayerScript>();
            p.GotShot();
        }
    }

}
