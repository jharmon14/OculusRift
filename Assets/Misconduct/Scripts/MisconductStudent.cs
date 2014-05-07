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
using System.Collections.Generic;
using System.Linq;

public class MisconductStudent : MonoBehaviour
{

	// Inspector variables
	public bool isPlayer = false;
	public int modelNumber = 1;
	public Transform playerPrefab;

	// Public variables
	[HideInInspector]
	public bool revertColor = false;
	[HideInInspector]
	public bool highlightColor = false;
	[HideInInspector]
	public bool isIdle = true;

	// Private variables
	private float colorLerp = 0.0f;
	private MeshRenderer meshRend;
	private Transform model;

	void Start()
	{
		model = transform.Find("AM_student" + modelNumber);
		List<GameObject> modelParts = getModelParts();
		if (isPlayer)
		{
			modelParts.ForEach(p => p.SetActive(false));
			var playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
			var player = Instantiate(playerPrefab, playerPos, transform.rotation) as Transform;
			player.name = player.name.Replace("(Clone)", "");
			player.GetComponent<MisconductPlayerController>().playerModelParts = modelParts;
			//var mm = GameObject.Find("MisconductManager").GetComponent<MisconductManager>();
			//var player = Instantiate(mm.playerObject, this.transform.position, this.transform.rotation) as GameObject;
			//mm.player = player.GetComponent<MisconductPlayerController>();
			//mm.player.playerStudent = meshRend;
			//mm.player.playerModel = model;

		}
	}

	void Update()
	{
		if (isIdle)
		{
			model.animation.Play("student" + modelNumber + "_idle");
		}
		else
		{
			model.animation.Play("student" + modelNumber + "_leave");
			Debug.Log("here");
		}
		//if (revertColor)
		//{
		//  if (colorLerp < 1)
		//  {
		//    colorLerp += 5.0f * Time.deltaTime;
		//    meshRend.material.color = Color.Lerp(Color.green, Color.white, colorLerp);
		//  }
		//  else
		//  {
		//    revertColor = false;
		//    colorLerp = 0.0f;
		//  }
		//}

		//if (highlightColor)
		//{
		//  if (colorLerp < 1)
		//  {
		//    colorLerp += 5.0f * Time.deltaTime;
		//    meshRend.material.color = Color.Lerp(Color.white, Color.green, colorLerp);
		//  }
		//  else
		//  {
		//    highlightColor = false;
		//    colorLerp = 0.0f;
		//  }
		//}
	}

	public List<GameObject> getModelParts()
	{
		List<GameObject> parts = new List<GameObject>();
		for (int i = 0; i < model.childCount; i++)
		{
			var child = model.GetChild(i);
			if ((child.name != "Armature") &&
				(child.name != "table") && 
				(child.name != "Paper"))
			{
				parts.Add(child.gameObject);
			}
		}
		return parts;
	}
}
