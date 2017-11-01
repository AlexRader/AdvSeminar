using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bar_scr : MonoBehaviour 
{
	[SerializeField]
	private float fillAmount = 0;

	[SerializeField]
	private Image content = null;

	private string myName;
	// Use this for initialization
	void Start () 
	{
		myName = this.gameObject.name;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//HandleBar ();
	}

	//handles change over time mechanics
	private void HandleBar(Vector2 amounts)
	{
		if (content.fillAmount <= 0f) 
		{
			this.SendMessage (myName);
			amounts.x = 0f;
		}
		content.fillAmount = Map(amounts.x, amounts.y);


	}

	//translation for the fill amount
	private float Map(float val, float inputMax)
	{
		// takes whatever your current filled amount is and puts it between 0 and 1.
		return (val) / (inputMax);
	}

	float getFill()
	{
		return fillAmount;
	}
}
