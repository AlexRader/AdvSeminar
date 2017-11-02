using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_scr : MonoBehaviour {

	private GameObject playerRef;
	private Rigidbody2D myRB;
	private Vector3 velocity;
	private float maxSpeed = 5.0f;
	// Use this for initialization
	void Start () 
	{
		Physics2D.IgnoreLayerCollision(8, 10, true);
		myRB = GetComponent<Rigidbody2D> ();
		playerRef = GameObject.FindGameObjectWithTag("Player");
		//Vector3 relativePosition = playerRef.transform.position - this.transform.position;
		//Quaternion rotation = Quaternion.LookRotation(relativePosition);
		//transform.rotation = rotation;
		velocity = playerRef.transform.position - this.transform.position;
		myRB.velocity = velocity.normalized;
		myRB.velocity *= maxSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.gameObject.GetComponent<Timer_scr>().SendMessage("changeHealth");
			Destroy(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
