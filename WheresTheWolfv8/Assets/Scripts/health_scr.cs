using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_scr : MonoBehaviour {

	private float curAmount;
	private float maxAmount;
	private Vector2 vecAmount;

	// Use this for initialization
	void Start () 
	{
		//currStamina = 0.001f;
		maxAmount = 10.0f;
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
	}
	
	void modHP(float damage)
	{	
		vecAmount.x -= damage;
		this.SendMessage ("HandleBar", vecAmount);
	}

	void health()
	{
        GameObject.Find("ControlObject").SendMessage("gameOver");
    }
}
