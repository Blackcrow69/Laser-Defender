using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public int score = 0;
	private Text myText;
	
	// Use this for initialization
	void Score (int points) {
		score+=points;
		myText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Reset () {
		score = 0;
		myText.text = score.ToString();
	}
	
	void Start () {
		myText = GetComponent<Text>();
	}
}
