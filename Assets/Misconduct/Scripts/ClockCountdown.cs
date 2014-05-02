using UnityEngine;
using System.Collections;

public class ClockCountdown : MonoBehaviour 
{
	public int minutes, seconds;
	public TextMesh clockMinutes;
	public TextMesh clockSeconds;
	public GameObject light;
	
	private bool outOfTime = false;
	private int secondsPassed = 0;

	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(CountDown());
	}

	void Awake()
	{
		MisconductManager mm = GameObject.Find("MisconductManager").GetComponent<MisconductManager>();
		mm.startMins = minutes;
		mm.startSecs = seconds;
	}

	void Update()
	{
		if ((minutes <= 0) && (seconds <= 0))
		{
			GameObject.Find("MisconductManager").GetComponent<MisconductManager>().gameOver();
		}
	}
	
	IEnumerator CountDown()
	{
		if(minutes >= 0 && seconds > 30)
			light.light.intensity = 0;
		
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
			
			// turn red and flash every 2 seconds if running out of time
			if(minutes == 0 && seconds <= 30 && (secondsPassed % 2 == 0))
			{
				light.light.intensity = 2;
				light.light.color = Color.white;
				clockMinutes.renderer.material.color = Color.red;
				clockSeconds.renderer.material.color = Color.red;
			}
			
			else if(minutes == 0 && seconds <= 30 && (secondsPassed % 2 != 0))
			{
				light.light.intensity = 2;
				light.light.color = Color.red;
				clockMinutes.renderer.material.color = Color.white;
				clockSeconds.renderer.material.color = Color.white;		
			}
				
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
		secondsPassed++;
		
		StartCoroutine(CountDown());
	}

}
