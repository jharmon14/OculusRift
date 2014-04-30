﻿/************************************************************************************
 *
 * Filename :   MisconductManager.cs
 * Content  :   
 * Expects  :   
 * Authors  :   
 * 
************************************************************************************/

using UnityEngine;
using System.Collections;

public class MisconductManager : MonoBehaviour
{

	// Inspector variables
	public GameObject playerOject;
  [HideInInspector]
	public MisconductPlayerController player;
	[HideInInspector]
	public MisconductStudent playerStudent;
	
	private UISlider slider;
	

	private int answersCollected = 0;
	private int suspicionLevel = 0;

	public int maxSuspicion = 100;

	// Private variables

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}
	
	void Awake(){
		slider = GameObject.Find("SuspicionProgressBar").GetComponent<UISlider>();
	}
	
	public void increaseSuspicion(int val = 1){
		suspicionLevel += val;
		
		slider.sliderValue = suspicionLevel/100.0f;
		
		// Filled the bar
		if (slider.sliderValue >= 1.0f){
			Debug.Log("You got caught!!!");
		}
	}
	
	public void reduceSuspicion(int val = 1){
		suspicionLevel -= val;
		if(suspicionLevel < 0)
			suspicionLevel = 0;
		
		slider.sliderValue = suspicionLevel/100.0f;
	}

	public void collectAnswer()
	{
		answersCollected++;
	}
}
