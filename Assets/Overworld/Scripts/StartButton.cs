/************************************************************************************
 *
 * Filename :   StartButton.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{

	// Inspector variables
    public GameManager.Levels level;
	
	// Private variables
	
	
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("entered collider for " + level);
            other.gameObject.GetComponent<StartGame>().level = level;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("left collider for " + level);
            other.gameObject.GetComponent<StartGame>().level = GameManager.Levels.None;
        }
    }
}