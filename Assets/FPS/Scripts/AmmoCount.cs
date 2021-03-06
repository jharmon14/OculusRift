﻿using UnityEngine;
using System.Collections;

public class AmmoCount : MonoBehaviour
{
	private UILabel ammo;
	private int ammoCount;
	private bool reloading;
	private float reloadDone;
    private GameObject gameManager;
    private GameManager managerScript;

	public float reloadRate = 2.5f;
	public int ammoFull = 30;
	public bool isReloading;

	
	public AudioClip reloadSound;

	// Use this for initialization
	void Awake()
	{
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
		ammo = GetComponent<UILabel>();
		ammoCount = ammoFull;
		ammo.text = ammoFull.ToString() + " / " + ammoFull.ToString();
	}

	// Update is called once per frame
	void Update()
	{
        if (managerScript.paused)
            return;

		isReloading = reloading;
        bool paused = managerScript.paused;
        // check if paused
        if (paused)
            return;

		// check if player is reloading or firing
		if (!reloading)
		{
			// reload button pushed
			if (Input.GetButtonDown("Reload") && (ammoCount != ammoFull))
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
					ammo.text = ammoCount.ToString() + " / " + ammoFull.ToString();
				}
			}
		}

		// wait while player is reloading
		else if (Time.time > reloadDone)
		{
			ammo.text = ammoFull.ToString() + " / " + ammoFull.ToString();
			reloading = false;
		}
	}

	void Reload()
	{
		audio.PlayOneShot(reloadSound);
		
		ammoCount = ammoFull;
		reloading = true;
        isReloading = true;
		reloadDone = Time.time + reloadRate;
		ammo.text = "Reloading";
	}
}
