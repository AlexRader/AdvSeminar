using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class displayScore_scr : MonoBehaviour {

    public TextMeshProUGUI scoreText;
	// Use this for initialization
	void Start () {
		display();
	}

	void display()
	{
		scoreText.text = "Score: " + PlayerPrefs.GetInt("currScore");
	}
}
