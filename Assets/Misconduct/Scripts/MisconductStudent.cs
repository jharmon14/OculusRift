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
	[HideInInspector]
	public bool revertColor = false;
	[HideInInspector]
	public bool highlightColor = false;

	// Private variables
	private float colorLerp = 0.0f;
	private MeshRenderer meshRend;

	void Start()
	{
		meshRend = this.transform.Find("StudentShape").GetComponent<MeshRenderer>();
		if (isPlayer)
		{
      meshRend.enabled = false;
			var mm = GameObject.Find("MisconductManager").GetComponent<MisconductManager>();
			var player = Instantiate(mm.playerOject, this.transform.position, this.transform.rotation) as GameObject;
			mm.player = player.GetComponent<MisconductPlayerController>();
			mm.player.playerStudent = meshRend;
		}
	}

  void Update()
	{
		if (revertColor)
		{
			if (colorLerp < 1)
			{
				colorLerp += 5.0f * Time.deltaTime;
				meshRend.material.color = Color.Lerp(Color.green, Color.white, colorLerp);
			}
			else
			{
				revertColor = false;
				colorLerp = 0.0f;
			}
		}

		if (highlightColor)
		{
			if (colorLerp < 1)
			{
				colorLerp += 5.0f * Time.deltaTime;
				meshRend.material.color = Color.Lerp(Color.white, Color.green, colorLerp);
			}
			else
			{
				highlightColor = false;
				colorLerp = 0.0f;
			}
		}
	}
}
