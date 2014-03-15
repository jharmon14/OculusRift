using UnityEngine;
using System.Collections;

public class ShowLabel : MonoBehaviour
{
    private UILabel label;
    private string initText;
	private string game = "";
	private string genre = "";

    void Awake()
    {
        label = GameObject.Find("Label").GetComponent<UILabel>();
        initText = label.text;
		GameObject.Find("Label").GetComponent<UILabel>().enabled = false;
    }
	
	void Start()
	{
		GameObject.Find("Label").GetComponent<UILabel>().enabled = false;
	}

    void Update()
    {
		//label.text = initText.Replace("[GAME]", game);
		//label.text = initText.Replace("[GENRE]", genre);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StartFPS")
        {
			//game = "Rifle Training";
			//genre = "FPS";
			label.text = "Rifle Training - FPS \n\n Left click or press \"X\" on your controller to play";
			GameObject.Find("Label").GetComponent<UILabel>().enabled = true;
        }
		
		else if (other.gameObject.name == "StartMisconduct")
        {
			//game = "Academic Misconduct";
			//genre = "Stealth";
			label.text = "Academic Misconduct - Stealth \n\n Left Click or press \"X\" on your controller to play";
            GameObject.Find("Label").GetComponent<UILabel>().enabled = true;
        }
		
		else if (other.gameObject.name == "StartTimmy")
        {
			//game = "Little Timmy (Stuck in a Well)";
			//genre = "Platformer";
			label.text = "Little Timmy (Stuck in a Well) - Platformer \n\n Left Click or press \"X\" on your controller to play";
            GameObject.Find("Label").GetComponent<UILabel>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "StartFPS" || other.gameObject.name == "StartMisconduct" || other.gameObject.name == "StartTimmy")
            GameObject.Find("Label").GetComponent<UILabel>().enabled = false;
    }
}
