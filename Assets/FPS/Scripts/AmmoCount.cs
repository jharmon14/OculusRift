using UnityEngine;
using System.Collections;

public class AmmoCount : MonoBehaviour {

	private UILabel ammo;
	private int ammoCount;
	private int ammoFull;
	private bool reloading;
	private float reloadDone;

	public float reloadRate = 2.5f;

	// Use this for initialization
	void Awake() {
		ammo = GetComponent<UILabel>();
		ammoFull = int.Parse(ammo.text);
		ammoCount = ammoFull;
	}
	
	// Update is called once per frame
	void Update () 
	{
    // check if player is reloading or firing
		if (!reloading)
		{
      // reload button pushed
			if (Input.GetButtonDown("Reload"))
			{
				Reload();
			}

			// pulled the trigger
			if (Input.GetButtonDown("Fire1"))
			{
				// reload the magazine
				if (ammoCount == 0)
				{
					Reload();
				}
				else
				{
					ammoCount--;
					ammo.text = ammoCount.ToString();
				}
			}
		}

    // wait while player is reloading
		else
		{
			if (Time.time > reloadDone)
			{
				ammo.text = ammoFull.ToString();
				reloading = false;
			}
		}
	}

	void Reload()
	{
		ammoCount = ammoFull;
		reloading = true;
		reloadDone = Time.time + reloadRate;
		ammo.text = "Reloading";
	}
}
