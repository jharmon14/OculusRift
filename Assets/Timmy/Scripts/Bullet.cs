using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter()
    {
        Destroy(transform.parent.gameObject);
        //For Enemy bullets
        //GameObject go = GameObject.Find("Player");
        //PlayerJump pj = go.GetComponent<PlayerJump>();
        //pj.GotShot();
    }

}
