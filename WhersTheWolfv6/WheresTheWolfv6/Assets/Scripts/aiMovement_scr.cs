using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiMovement_scr : MonoBehaviour 
{
	private const float DRAG = 1.1f;
	private const float STOPSPEED = .2f;

	private Rigidbody2D myRB;
	private Vector3 endPos;
	private GameObject myObject;
	private int x;
	private int calc;
	private int MAX_SPEED = 2;

	private bool alive;
	private float deathtimer = 2.0f;


	private Vector3 velocity;

	public int confinedTo;

	//bool happen = false;
	// Use this for initialization
	void Start () 
	{
		myRB = GetComponent<Rigidbody2D> ();
		endPos = this.transform.position;
		alive = true;
		//moveTo ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (alive) 
		{
			move ();
		}
		else 
		{
			deathtimer -= Time.deltaTime;
			myRB.velocity = myRB.velocity / DRAG;
			if (myRB.velocity.magnitude <= STOPSPEED)
				myRB.velocity = Vector2.zero;
			if (deathtimer <= 0)
				death ();
		}
	}
	void FixedUpdate()
	{
		if (myRB.velocity.magnitude > MAX_SPEED && alive) 
		{
			myRB.velocity = myRB.velocity.normalized * MAX_SPEED;
		}
	}

	/*
	else
	{
		//this else will be using a switch for ww within a radius an flee from it
		mLinear = mpMover->getPosition() - mpTarget->getPosition();
	}*/
	void moveTo()
	{
		/*
			x = GameObject.Find ("aiControl").GetComponent<aiControl_scr> ().firstMarket.Length;
			//Debug.Log (x);
			//x -= 1;
			calc = Random.Range (0, x);
//			Debug.Log (calc);
		//	Debug.Log ("here");
		myObject = GameObject.Find ("aiControl").GetComponent<aiControl_scr> ().firstMarket [calc];
		endPos = myObject.transform.position;*/
		myObject.SendMessage ("returnNextMove", this.gameObject);
		//endPos = 
	}

	void move()
	{
		if (this.transform.position.x - endPos.x < 1f && this.transform.position.x - endPos.x > -1f) 
		{
			if (this.transform.position.y - endPos.y < 1f && this.transform.position.y - endPos.y > -1f) 
			{
				moveTo ();
			}
		}
		velocity = endPos - this.transform.position;
		//velocity /= inc;
		myRB.velocity = velocity;
		//velocity *= Time.deltaTime;
	}

	void death()
	{
		Destroy (this.gameObject);
	}


	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "Player" && coll.GetComponent<TransformAbilities_scr>().attack == true) 
		{
			//Debug.Log ("collided");
			myRB.velocity = coll.GetComponent<movement_scr> ().returnVelocity;
			alive = false;
			//death ();
		}
	}

	void setConfined(int var)
	{
		confinedTo = var;
	}

	void setMyObject(GameObject var)
	{
		myObject = var;
	}
	void setEndPos(Vector3 var)
	{
		endPos = var;
	}
}
