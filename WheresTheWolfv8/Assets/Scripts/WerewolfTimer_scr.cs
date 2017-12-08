using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfTimer_scr : MonoBehaviour 
{
	private bool check = false;
    private GameObject reference;
    private float curAmount;
	private float maxAmount;
	private Vector2 vecAmount;
	[SerializeField]
	private float currStamina;

	// Use this for initialization
	void Start () 
	{
		currStamina = 0f;
		maxAmount = 2.0f;
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
        reference = GameObject.FindGameObjectWithTag("Player");
    }

	// unique timer variable for Werewolf time.
	void incTime(float dt)
	{
		if (reference.GetComponent<TransformAbilities_scr>().werewolfForm == false) 
		{
			vecAmount.x -= dt;
            if (vecAmount.x > 0.0f)
            {
                reference.GetComponent<Timer_scr>().SendMessage("modHP", -0.01f);
            }
            if (vecAmount.x < 0)
                vecAmount.x = 0;
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
        if (vecAmount.x > vecAmount.y)
            vecAmount.x = vecAmount.y;
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
	}
		
	void resetMe ()
	{
		check = false;
	}

	void clearMe()
	{
		check = true;
	}
}
