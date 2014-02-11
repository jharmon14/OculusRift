/************************************************************************************
 *
 * Filename :   MisconductStudent.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class MisconductStudent : MonoBehaviour {

	// Inspector variables
    public bool isPlayer = false;
	
	// Private variables
	
	
	void Start () {
        if (isPlayer)
        {
            MakePlayer();
            var mm = GameObject.Find("MisconductManager").GetComponent<MisconductManager>();
            var player = Instantiate(mm.playerOject, transform.position, transform.rotation) as GameObject;
            mm.player = player.GetComponent<MisconductPlayerController>();
        }
	}

    void MakePlayer()
    {
        isPlayer = true;
        transform.Find("StudentShape").gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void NotPlayer()
    {
        isPlayer = false;
        transform.Find("StudentShape").gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
