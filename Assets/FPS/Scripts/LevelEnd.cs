using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour 
{
  private bool inLevelEnd = false;

	void Update()
	{
		if (inLevelEnd && Input.GetButtonDown("Reload"))
		{
			GameObject.Find("FPSManager").GetComponent<FPSManager>().LevelEnd(Time.time);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		inLevelEnd = true;
	}

	void OnTriggerExit(Collider other)
	{
		inLevelEnd = false;
	}
}
