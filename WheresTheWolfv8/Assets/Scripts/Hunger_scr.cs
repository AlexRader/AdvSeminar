using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger_scr : MonoBehaviour 
{
	private bool check = false;
	private float curAmount;
	private float maxAmount;
	private Vector2 vecAmount;
	// Use this for initialization
	void Start () 
	{
		maxAmount = 10;
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
	}
	
	//each timer has it's unique increment in time
	void incTime(float dt)
	{
		if (check != true) 
		{
			vecAmount.x -= dt;
			this.SendMessage ("HandleBar", vecAmount);
		}

	}
	// sends what to do to the timer in WerewolfTime
	void hunger()
	{
		check = !check;
		if (check == true)
			GameObject.Find("WerewolfTime").SendMessage ("modSpeedMultiply", 0.5f);
		else
			GameObject.Find("WerewolfTime").SendMessage ("modSpeedMultiply", 2.0f);

	}

	void resetMe ()
	{
		Debug.Log("here now bitch");

		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
		this.SendMessage ("reset", vecAmount);
		hunger();
	}
}
