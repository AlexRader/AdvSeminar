using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class savedVariables_scr : MonoBehaviour {

	private float MAX_TIME = .25f;
	private float startTime;

	public static int score = 0;
	private bool counting = false;
	Scene scene;
	public int scoreEnd;

	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene();
		if (scene.name == "GameOver")
		{
			scoreEnd = score;
		}
		startTime = MAX_TIME;
		getCurrentVars();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (counting == true)
		{
			startTime -= Time.deltaTime;
			if (startTime <= 0)
			{
				startTime = MAX_TIME;
				modScore(1);
			}
			GameObject.FindGameObjectWithTag("Player").SendMessage("changeScore", score);
		}
		//Debug.Log(score);
	}

	void Awake ()
	{
		//DontDestroyOnLoad(this.gameObject);
	}

	void saveCurrentVars()
	{
		PlayerPrefs.SetInt("currScore", score);
	}

	void getCurrentVars()
	{
		score = PlayerPrefs.GetInt("currScore");

	}

	void clearStored()
	{
		//Debug.Log("made it");
		PlayerPrefs.SetInt("currScore", 0);
		getCurrentVars();
		//Debug.Log(score);

	}

	void setBool()
	{
		counting = !counting;
		startTime = MAX_TIME;
	}

	void modScore(int var)
	{
		score += var;
		GameObject.FindGameObjectWithTag("Player").SendMessage("changeScore", score);
	}
}
