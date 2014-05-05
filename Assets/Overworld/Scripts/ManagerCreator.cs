using UnityEngine;
using System.Collections;

public class ManagerCreator : MonoBehaviour {

    public Font font;
    public TextMesh RTScore;
    public TextMesh LTScore;
    public TextMesh AMScore;

    public GameObject gameManager;
    private GameManager managerScript;

	// Use this for initialization
	void Start () {

        if (GameObject.Find("GameManager") == null)
        {
            GameObject GameManager = Instantiate(gameManager) as GameObject;
            GameManager.name = "GameManager";
        }
        
        managerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (managerScript.score.Length >= 1)
        {
            RTScore.text = managerScript.score[(int)GameManager.Levels.FPS].ToString();
            LTScore.text = (managerScript.score[(int)GameManager.Levels.Timmy] + managerScript.score[(int)GameManager.Levels.Timmy2] + managerScript.score[(int)GameManager.Levels.Timmy3]).ToString();
			AMScore.text = managerScript.score[(int)GameManager.Levels.Misconduct].ToString();
            //AMScore.text = gm.score[(int)GameManager.Levels.Misconduct].ToString();
        }
	}
}
