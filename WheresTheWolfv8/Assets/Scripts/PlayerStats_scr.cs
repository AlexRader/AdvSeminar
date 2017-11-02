using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats_scr : MonoBehaviour 
{
	[SerializeField]
	private float strength;

	[SerializeField]
	private float speed;

	[SerializeField]
	private float stamina;

	// Use this for initialization
	void Start () 
	{
		strength = 10f;
		speed = 10f;
		stamina = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		//UpdateStamina (0f);
	}

	void UpdateStrength (float var)
	{
		strength += var;
	}

	void UpdateSpeed (float var)
	{
		speed += var;
	}
	//update the stamina
	void UpdateStamina (float var)
	{
		stamina += var;
		//Debug.Log (stamina);
		GameObject.Find ("WerewolfTime").SendMessage ("modSpeedAdd", stamina);
	}
}
