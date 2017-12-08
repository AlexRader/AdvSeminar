using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_scr : MonoBehaviour {

	private GameObject playerRef;
	private Rigidbody2D myRB;
	private Vector3 velocity;
	private float maxSpeed = 25.0f;
	// Use this for initialization
	void Start () 
	{
		Physics2D.IgnoreLayerCollision(8, 10, true);
		myRB = GetComponent<Rigidbody2D> ();
		playerRef = GameObject.FindGameObjectWithTag("Player");

		Vector3 relativePosition = (playerRef.transform.position - this.transform.position);
		relativePosition.Normalize();

		float rotZ = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ - 90.0f);

		velocity = playerRef.transform.position - this.transform.position;
		myRB.velocity = velocity.normalized;
		myRB.velocity *= maxSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.gameObject.GetComponent<Timer_scr>().SendMessage("changeHealth");
            coll.gameObject.GetComponent<colorLerp_scr>().SendMessage("startRoutine");
            Destroy(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
