using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfTimer_scr : MonoBehaviour 
{
	private bool check = false;
	private float curAmount;
	private float maxAmount;
	private Vector2 vecAmount;
	[SerializeField]
	private float currStamina;

	// Use this for initialization
	void Start () 
	{
		currStamina = 0.001f;
		maxAmount = 10.0f;
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	// unique timer variable for Werewolf time.
	void incTime(float dt)
	{
		if (check != true) 
		{
			vecAmount.x -= dt;
			this.SendMessage ("HandleBar", vecAmount);
		}
		else
		{
			addTo(dt);
		}
	}

	void addTo(float dt)
	{
		vecAmount.x += dt;
		this.SendMessage("HandleBarIcrease", vecAmount);
	}


	void modSpeedMultiply(float var)
	{
		vecAmount *= var;
	}

	void modSpeedAdd(float var)
	{
		var -= currStamina;
		vecAmount.x += currStamina;
		vecAmount.y += currStamina;
		//Debug.Log (currStamina);
	}

	void WerewolfTime ()
	{
		check = !check;
		GameObject.FindGameObjectWithTag ("Player").SendMessage("changeForm");
	}
		
	void resetMe ()
	{
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
		this.SendMessage ("reset", vecAmount);
		check = false;
	}

	void clearMe()
	{
		curAmount = 0.0f;
		vecAmount = new Vector2(curAmount, maxAmount);
		this.SendMessage("reset", vecAmount);
		check = true;
	}
}
