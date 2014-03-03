using UnityEngine;
using System.Collections;

public class TimmyManager : MonoBehaviour {

    public float totalTime;
    public float lastTime;
    public int points;
    public int lives;

    void Awake()
    {
        // Fade in the camera
        CameraFade.StartAlphaFade(Color.black, true, 4.0f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
