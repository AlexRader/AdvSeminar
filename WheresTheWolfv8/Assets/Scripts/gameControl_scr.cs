using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameControl_scr : MonoBehaviour 
{

	Scene scene;
	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene();
//		Debug.Log(scene.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void levelChange ()
	{
		GameObject.FindGameObjectWithTag("variables").SendMessage("saveCurrentVars");
		if (scene.name == "Main Scene")
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	void gameOver ()
	{
		GameObject.FindGameObjectWithTag("variables").SendMessage("saveCurrentVars");
		if (scene.name == "Main Scene")
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}



}
