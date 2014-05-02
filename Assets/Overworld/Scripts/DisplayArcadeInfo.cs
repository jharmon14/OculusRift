using UnityEngine;
using System.Collections;

public class DisplayArcadeInfo : MonoBehaviour 
{
	public Font font;
	public TextMesh RTScore;
	public TextMesh LTScore;
	public TextMesh AMScore;
	
	public GameObject gm;
    private GameManager managerScript;
	
	void Start () 
	{

	}
	
	void Update () 
	{
		
	}
	
	void Awake()
	{
        //gm = GameObject.Find("GameManager");
		managerScript = gm.GetComponent<GameManager>();
        if (managerScript.score.Length >= 1)
        {
            int timmyScore = managerScript.score[(int)GameManager.Levels.Timmy] + managerScript.score[(int)GameManager.Levels.Timmy2] + managerScript.score[(int)GameManager.Levels.Timmy3];
            Debug.Log(timmyScore);
            RTScore.text = managerScript.score[(int)GameManager.Levels.FPS].ToString();
            LTScore.text = timmyScore.ToString();
            //AMScore.text = gm.score[(int)GameManager.Levels.Misconduct].ToString();
        }
	}

}
