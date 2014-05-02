using UnityEngine;
using System.Collections;

public class TimmyManager : MonoBehaviour {

    public float totalTime;
    public float lastTime;
    public int points;
    public int lives;
    public int kills;

    void Awake()
    {
        // Fade in the camera
        CameraFade.StartAlphaFade(Color.black, true, 4.0f);
		Screen.showCursor = false;
    }

	// Use this for initialization
	void Start () {
        kills = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Screen.showCursor = false;
	}
}
