using UnityEngine;
using System.Collections;

public class DisplayArcadeInfo : MonoBehaviour 
{
	public Font font;
	public TextMesh RTScore;
	public TextMesh LTScore;
	public TextMesh AMScore;
	
	private GameManager gm;
	
	void Start () 
	{

	}
	
	void Update () 
	{
		
	}
	
	void Awake()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		RTScore.text = gm.score[1].ToString();
		//LTScore.text = gm.score[2].ToString();
		//AMScore.text = gm.score[3].ToString();
	}

}
