using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer_scr : MonoBehaviour 
{
	private const float MAXTIME = 90;
	private float curTime;
	private float deltaTime;
	private GameObject[] timers;

	public Text scoreText;
	// Use this for initialization
	void Start () 
	{
		curTime = MAXTIME;
		if (timers == null) 
			timers = GameObject.FindGameObjectsWithTag("timer");
        Debug.Log(timers.Length);
        for (int i = 0; i < timers.Length; ++i)
        {
            Debug.Log(timers[i].name);
        }

    }
	
	// Update is called once per frame
	void Update () 
	{
		deltaTime = Time.deltaTime;
		curTime -= deltaTime;
		for (int i = 0; i < timers.Length; ++i)
		{
			if (timers[i].name != "health")
			{
				timers [i].SendMessage ("incTime", deltaTime);
			}
		}
	}
	void changeScore(int var)
	{
		scoreText.text = "Score: " + var;
	}

	void reset()
	{
		if (timers != null)
		{
			for (int i = 0; i < timers.Length; ++i)
			{
				if (timers[i].name != "health" && timers[i].name != "levelEnd")
					timers[i].SendMessage("resetMe");
			}
		}
	}
	void clear()
	{
		for (int i = 0; i < timers.Length; ++i)
		{
			if (timers[i].name != "health" && timers[i].name != "levelEnd")
				timers[i].SendMessage("clearMe");
		}
	}

	void changeHealth()
	{
		for (int i = 0; i < timers.Length; ++i)
		{
			if (timers[i].name == "health")
				timers [i].SendMessage ("modHP", 1.0f);
		}
	}

    void modHP(float var)
    {
        for (int i = 0; i < timers.Length; ++i)
        {
            if (timers[i].name == "health")
                timers[i].SendMessage("modHP", var);
        }
    }
		
}
