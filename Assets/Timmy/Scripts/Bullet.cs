using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public bool direction;
    public PlayerMovement playerMovement;
    public GameObject playerParent;

    private GameObject pauseManager;
    private PauseManagerScript managerScript;
    private bool paused;
    private Enemy enemyScript;

	// Use this for initialization
	void Start () {
        pauseManager = GameObject.Find("PauseManager");
        managerScript = pauseManager.GetComponent<PauseManagerScript>();
        if (tag == "PlayerBullet")
        {
            playerParent = GameObject.Find("PlayerParent");
            playerMovement = playerParent.GetComponent<PlayerMovement>();
            direction = playerMovement.direction;
        }
	}
	
	// Update is called once per frame
	void Update () {
        paused = managerScript.paused;
        if (paused)
            return;

        if (direction)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 3);
        }
        else
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -3);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        //For player bullets
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerBullet")
        {
			Debug.Log("here");
            collision.gameObject.GetComponent<Enemy>().GotShot();
        }
        //For Enemy bullets
        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet")
        {
            collision.gameObject.GetComponent<PlayerScript>().GotShot();
        }
        Destroy(gameObject);
    }

    public void SetDirection(bool dir)
    {
        direction = dir;
    }

}
