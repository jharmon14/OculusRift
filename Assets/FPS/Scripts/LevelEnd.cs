using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour
{
    private FPSManager fpsm;
	private GameManager gm;
    private bool inLevelEnd = false;
    private UILabel label;
    private string initText;

    void Awake()
    {
        fpsm = GameObject.Find("FPSManager").GetComponent<FPSManager>();
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        label = GameObject.Find("LevelEndLabel").GetComponent<UILabel>();
        initText = label.text;
    }

    void Update()
    {
        label.text = initText.Replace("[TARGETS]", fpsm.targetsHit.ToString());
        label.text = label.text.Replace("[ACCURACY]", (fpsm.accuracy * 100).ToString() + "%");
        label.text = label.text.Replace("[SCORE]", fpsm.score.ToString());

        if (inLevelEnd && Input.GetButtonDown("Reload"))
        {
            fpsm.LevelEnd(Time.time);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OVRPlayerControllerWithToggle")
        {
            inLevelEnd = true;
            fpsm.timeEnded = Time.time;
            GameObject.Find("LevelEndLabel").GetComponent<UILabel>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OVRPlayerControllerWithToggle")
        {
            inLevelEnd = false;
            GameObject.Find("LevelEndLabel").GetComponent<UILabel>().enabled = false;
        }
    }
}
