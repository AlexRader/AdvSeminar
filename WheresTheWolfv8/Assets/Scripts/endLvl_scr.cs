using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLvl_scr : MonoBehaviour
{

	private bool check = false;
	private float curAmount;
	private float maxAmount;
	private Vector2 vecAmount;
	private Vector3 currentPercentage;
	// Use this for initialization
	void Start()
	{
		maxAmount = 60;
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
		currentPercentage = new Vector3(maxAmount, vecAmount.x, 180.0f / maxAmount);
	}

	// Update is called once per frame
	void Update()
	{

	}
	//each timer has it's unique increment in time
	void incTime(float dt)
	{
		if (check != true)
		{
			vecAmount.x -= dt;
			currentPercentage.y = vecAmount.x;
			this.SendMessage("percentageFilled", currentPercentage);
			this.SendMessage("HandleBar", vecAmount);
		}

	}

	void levelEnd()
	{
		//		Debug.Log("working");
		GameObject.Find("ControlObject").SendMessage("levelChange");
	}
}
