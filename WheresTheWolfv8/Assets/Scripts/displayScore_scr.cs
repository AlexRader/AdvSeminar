using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayScore_scr : MonoBehaviour {

	public Text scoreText;
	// Use this for initialization
	void Start () {
		display();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void display()
	{
		scoreText.text = "Score: " + PlayerPrefs.GetInt("currScore");
	}
}
