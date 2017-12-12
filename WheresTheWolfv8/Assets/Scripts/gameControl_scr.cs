using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameControl_scr : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		
	}
	

	void levelChange ()
	{
		GameObject.FindGameObjectWithTag("variables").SendMessage("saveCurrentVars");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}

	void gameOver ()
	{
		GameObject.FindGameObjectWithTag("variables").SendMessage("saveCurrentVars");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}


}
