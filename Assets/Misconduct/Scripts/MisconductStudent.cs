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

public class MisconductStudent : MonoBehaviour
{

	// Inspector variables
	public bool isPlayer = false;

	// Private variables


	void Start()
	{
		if (isPlayer)
		{
			MeshRenderer meshRend = this.transform.Find("StudentShape").GetComponent<MeshRenderer>();
      meshRend.enabled = false;
			var mm = GameObject.Find("MisconductManager").GetComponent<MisconductManager>();
			var player = Instantiate(mm.playerOject, this.transform.position, this.transform.rotation) as GameObject;
			mm.player = player.GetComponent<MisconductPlayerController>();
			mm.player.playerStudent = meshRend;
		}
	}

	public void ToggleMesh()
	{
		var meshRend = transform.Find("StudentShape").GetComponent<MeshRenderer>();
		meshRend.enabled = !meshRend.enabled;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
