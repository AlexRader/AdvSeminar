using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_scr : MonoBehaviour 
{
	private const float MAXTIME = 90;
	private float curTime;
	private float deltaTime;
	private GameObject[] timers;
	// Use this for initialization
	void Start () 
	{
		curTime = MAXTIME;
		if (timers == null) 
			timers = GameObject.FindGameObjectsWithTag("timer");
//		Debug.Log (timers.Length);
	}
	
	// Update is called once per frame
	void Update () 
	{
		deltaTime = Time.deltaTime;
		curTime -= deltaTime;
		for (int i = 0; i < timers.Length; ++i)
			timers [i].SendMessage ("incTime", deltaTime);
	}
}
