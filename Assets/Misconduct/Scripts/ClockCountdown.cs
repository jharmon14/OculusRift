using UnityEngine;
using System.Collections;

public class ClockCountdown : MonoBehaviour 
{
	public int minutes, seconds;
	public TextMesh clockMinutes;
	public TextMesh clockSeconds;
	
	private bool outOfTime = false;

	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(CountDown());
	}
	
	IEnumerator CountDown()
	{
		if(seconds == 0 && minutes == 0)
			outOfTime = true;
		
		if(!outOfTime)
		{
			if(seconds <= 0)
			{
				seconds = 59;
				minutes --;			
			}
				
			else
				seconds--;
				
			// add a dummy 0 to m
			if(minutes < 10)
				clockMinutes.text = "0" + minutes.ToString() + ":";
			
			else
				clockMinutes.text = minutes.ToString() + ":";
			
			// add a dummy 0 to s
			if(seconds < 10)
				clockSeconds.text = "0" + seconds.ToString();
			
			else
				clockSeconds.text = seconds.ToString();		
		}
		
		yield return new WaitForSeconds(1);
		
		StartCoroutine(CountDown());
	}

}
