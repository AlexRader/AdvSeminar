using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_scr : MonoBehaviour 
{
	private float MAX_VELOCITY = 6.0f;
	private float inc;
	private float maxVel;
	private float curVel;
	private Rigidbody2D myRB;

	private Vector3 moveInput;

	private Vector3 moveVelocity;
	public Vector3 returnVelocity;
	private bool dash;
	// Use this for initialization
	void Start () 
	{
		myRB = GetComponent<Rigidbody2D> ();
		inc = .2f;
		maxVel = 10;
		curVel = 0;
		dash = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        playerInput ();
		returnVelocity = myRB.velocity;

	}

	void ModVelocity ()
	{
		curVel += inc;
		if (curVel > maxVel)
			curVel = maxVel;
	}

	void playerInput()
	{
		float axisX = Input.GetAxis ("Horizontal");
		float axisY = Input.GetAxis ("Vertical");

        if (dash == false)
        {
            moveInput = new Vector3 (axisX, axisY);
		    moveVelocity = moveInput * curVel;

            if (Input.GetKey(KeyCode.W))
                ModVelocity();
            if (Input.GetKey(KeyCode.S))
                ModVelocity();
            if (Input.GetKey(KeyCode.A))
                ModVelocity();
            if (Input.GetKey(KeyCode.D))
                ModVelocity();


            myRB.velocity = moveVelocity;
            if (myRB.velocity.magnitude > MAX_VELOCITY)
            {
                myRB.velocity = myRB.velocity.normalized * MAX_VELOCITY;
            }
        }

        SendMessage("werewolfInputs");
    }

	void changeMaxVel(float var)
	{
		MAX_VELOCITY += var * .5f;
		maxVel += var;
		curVel += var;
	}

	void disableInput()
	{
		dash = !dash;
	}

}
