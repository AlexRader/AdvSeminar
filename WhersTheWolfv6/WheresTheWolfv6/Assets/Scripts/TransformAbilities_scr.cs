﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// everything to do with dashing mechanic other than the message calls and Rigidbody2D was created here http://answers.unity3d.com/questions/892955/dashing-mechanic-using-rigidbodyaddforce.html
// by http://answers.unity3d.com/users/520198/alfalfasprout.html

public class TransformAbilities_scr : MonoBehaviour 
{

	private bool werewolfForm;

	public DashState dashState;
	private float dashTimer;
	public BiteState biteState;
	private float biteTimer;

	private float maxDash = .1f;
	private float maxBite = 1.0f;

	private float cooldownTimer = 5f;

	public Vector2 savedVelocity;

	private Rigidbody2D rb;

	public bool attack = false;


	public enum DashState 
	{
		Ready,
		Dashing,
		Cooldown
	}
	public enum BiteState
	{
		Ready,
		Biting,
	}

	void Biting()
	{
		switch (biteState) 
		{
		case BiteState.Ready:
			var isBiteKeyDown = Input.GetKey (KeyCode.C);
			if (isBiteKeyDown) 
			{
				savedVelocity = rb.velocity;
				attack = true;
				rb.velocity = Vector2.zero;
				biteState = BiteState.Biting;
				GameObject.FindGameObjectWithTag ("Player").SendMessage ("disableInput");
			}
			break;
		case BiteState.Biting:
			biteTimer -= Time.deltaTime;
			if (biteTimer <= 0) 
			{
				attack = false;
				biteTimer = maxBite;
				rb.velocity = savedVelocity;
				GameObject.FindGameObjectWithTag ("Player").SendMessage ("disableInput");
				biteState = BiteState.Ready;
			}
			break;
		}
	}

	void Dashing()
	{
		switch (dashState) 
		{
		case DashState.Ready:
			var isDashKeyDown = Input.GetKey (KeyCode.V);
			if(isDashKeyDown)
			{
				savedVelocity = rb.velocity;
				attack = true;
				rb.velocity =  new Vector2(rb.velocity.x * 3f, rb.velocity.y * 3f);
				dashState = DashState.Dashing;
				GameObject.FindGameObjectWithTag ("Player").SendMessage("disableInput");
			}
			break;
		case DashState.Dashing:
			dashTimer -= Time.deltaTime;
			if(dashTimer <= 0)
			{
				attack = false;
				dashTimer = cooldownTimer;
				rb.velocity = savedVelocity;
				dashState = DashState.Cooldown;
				GameObject.FindGameObjectWithTag ("Player").SendMessage("disableInput");
			}
			break;
		case DashState.Cooldown:
			dashTimer -= Time.deltaTime;
			if(dashTimer <= 0)
			{
				dashTimer = maxDash;
				dashState = DashState.Ready;
				attack = false;
			}
			break;
		}
	}
	// Use this for initialization
	void Start () 
	{
		werewolfForm = false;
		dashTimer = maxDash;
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (werewolfForm == true) 
		{ 
			Dashing ();
			Biting ();
		}
	}

	void newInputs()
	{
		//if (Input.GetKey (KeyCode.C))
	}

	void changeForm()
	{
		werewolfForm = !werewolfForm;

		if (werewolfForm == true) 
			GameObject.FindGameObjectWithTag ("Player").SendMessage("changeMaxVel", 10f);
		else
			GameObject.FindGameObjectWithTag ("Player").SendMessage("changeMaxVel", -10f);


	}
}
