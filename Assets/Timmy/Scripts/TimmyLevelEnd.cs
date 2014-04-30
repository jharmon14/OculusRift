using UnityEngine;
using System.Collections;

public class TimmyLevelEnd : MonoBehaviour {

    private bool inLevelEnd = false;
    private UILabel label;
    private string initText;
    private GameManager gm;
    private PlayerScript ps;
    private int stopTime;
    private bool calc = true;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        label = GameObject.Find("LevelEndLabel").GetComponent<UILabel>();
        label.enabled = false;
        initText = label.text;
    }

    void Update()
    {
        if (inLevelEnd)
        {
            gm.paused = true;
            if (calc)
            {
                gm.score[(int)GameManager.Levels.Timmy + gm.timmyCurrentLevel] += ps.CalculateFinalScore(stopTime);
                calc = false;
            }
            label.text = label.text.Replace("[SCORE]", gm.score[(int)GameManager.Levels.Timmy + gm.timmyCurrentLevel].ToString());

            if (Input.GetButtonDown("Jump"))
            {
                GameObject.Find("Player").GetComponent<PlayerScript>().NextLevel();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLevelEnd = true;
            stopTime = int.Parse(ps.gameTime.text);
            GameObject.Find("LevelEndLabel").GetComponent<UILabel>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLevelEnd = false;
            GameObject.Find("LevelEndLabel").GetComponent<UILabel>().enabled = false;
        }
    }
}
