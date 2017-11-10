using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// everything to do with dashing mechanic other than the message calls and Rigidbody2D was created here http://answers.unity3d.com/questions/892955/dashing-mechanic-using-rigidbodyaddforce.html
// by http://answers.unity3d.com/users/520198/alfalfasprout.html

public class TransformAbilities_scr : MonoBehaviour 
{

	public bool werewolfForm;

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

	private Component test;

	private bool changeBack = true;

	private GameObject playerRef;
	private GameObject cameraRef;

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
			var isBiteKeyDown = Input.GetKeyDown (KeyCode.J);
			if (isBiteKeyDown) 
			{
				savedVelocity = rb.velocity;
				attack = true;
				rb.velocity = Vector2.zero;
				biteState = BiteState.Biting;
				playerRef.SendMessage ("disableInput");
				gameObject.GetComponent<CircleCollider2D>().radius = 1f;
				cameraRef.SendMessage("shakingRoutine", false);
			}
			break;
		case BiteState.Biting:
			biteTimer -= Time.deltaTime;
			if (biteTimer <= 0) 
			{
				attack = false;
				biteTimer = maxBite;
				rb.velocity = savedVelocity;
				playerRef.SendMessage ("disableInput");
				cameraRef.SendMessage("shakingRoutine", false);
				gameObject.GetComponent<CircleCollider2D>().radius = 3f;
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
			var isDashKeyDown = Input.GetKeyDown (KeyCode.K);
			if(isDashKeyDown)
			{
				savedVelocity = rb.velocity;
				attack = true;
				rb.velocity =  new Vector2(rb.velocity.x * 3f, rb.velocity.y * 3f);
				dashState = DashState.Dashing;
				playerRef.SendMessage("disableInput");
				gameObject.GetComponent<CircleCollider2D>().radius = 1f;
				cameraRef.SendMessage("shakingRoutine", true);
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
				playerRef.SendMessage("disableInput");
				cameraRef.SendMessage("shakingRoutine", true);
				gameObject.GetComponent<CircleCollider2D>().radius = 3f;	
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
		changeForm();
		dashTimer = maxDash;
		rb = GetComponent<Rigidbody2D> ();
		test = GameObject.FindGameObjectWithTag("variables").GetComponent<savedVariables_scr>();
		playerRef = GameObject.FindGameObjectWithTag("Player");
		cameraRef = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (werewolfForm == true) 
		{ 
			Dashing ();
			Biting ();
		}
		newInputs();
	}

	void newInputs()
	{
		if (Input.GetKeyDown (KeyCode.L) && changeBack == true)
		{
			changeForm();
			allowChange(true);
		}
	}

	void allowChange(bool var)
	{
		changeBack = var;
	}

	void changeForm()
	{
		werewolfForm = !werewolfForm;

		if (werewolfForm == true)
		{
			GameObject.FindGameObjectWithTag("Player").SendMessage("changeMaxVel", 10f);
			this.GetComponent<Timer_scr>().SendMessage("reset");
			//Debug.Log("toomuch");
		}
		else
		{
			GameObject.FindGameObjectWithTag("Player").SendMessage("changeMaxVel", -10f);
			this.GetComponent<Timer_scr>().SendMessage("clear");
			biteTimer = 0;
			dashTimer = 0;
			Dashing();
			Biting();
		}
		GameObject.FindGameObjectWithTag("variables").GetComponent<savedVariables_scr>().SendMessage("setBool");
		GameObject.FindGameObjectWithTag("Player").SendMessage("changeInteraction", werewolfForm);
	}
}
