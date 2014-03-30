using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour
{

	// Use this for initialization
	void Start () 
	{
		MovieTexture movie = renderer.material.mainTexture as MovieTexture;
		audio.clip = movie.audioClip;
		
		movie.Play();
		//audio.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
