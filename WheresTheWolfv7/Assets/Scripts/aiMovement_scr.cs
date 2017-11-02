using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiMovement_scr : MonoBehaviour
{
	private const int MAX_SPEED_MODIFY = 20;
	private const int MIN_SPEED_MODIFY = 1;

	private const float DRAG = 1.1f;
	private const float STOPSPEED = .2f;

	private Rigidbody2D myRB;
	private Vector3 endPos;
	//private GameObject myObject;
	private float maxSpeed = 2;

	private bool alive;

	private float deathtimer = 2.0f;

	private float modVelocity;

	private string movementType = "";

	private Vector3 velocity;

	private Vector3 distance;
	private float actualDistance;

	public bool confinedTo;

	public GameObject[] myArea;

	private int myLocation = 0;

	private bool run = false;
	RaycastHit2D hit;
	int layermask = 1 << 10;

	private GameObject playerRef;

	private Component test;

	private const float MAXTIME = 1.0f;
	private float curTime = 0;
	void Start()
	{
		myRB = GetComponent<Rigidbody2D>();
		endPos = this.transform.position;
		Physics2D.IgnoreLayerCollision(8, 10, true);
		Physics2D.IgnoreLayerCollision(8, 8, true);
		Physics2D.IgnoreLayerCollision(8, 0, true);
		alive = true;
		modVelocity = (Random.Range(MIN_SPEED_MODIFY, MAX_SPEED_MODIFY)) / 20.0f;
		maxSpeed = maxSpeed + modVelocity + modVelocity + modVelocity;
		playerRef = GameObject.FindGameObjectWithTag("Player");
		test = GameObject.FindGameObjectWithTag("variables").GetComponent<savedVariables_scr>();
	}

	// Update is called once per frame
	void Update()
	{
		if (alive)
		{
			move();
		}
		else
		{
			deathtimer -= Time.deltaTime;
			myRB.velocity = myRB.velocity / DRAG;
			if (myRB.velocity.magnitude <= STOPSPEED)
				myRB.velocity = Vector2.zero;
			if (deathtimer <= 0)
				death();
		}
	}
	void FixedUpdate()
	{
		if (myRB.velocity.magnitude > maxSpeed && alive)
		{
			myRB.velocity = myRB.velocity.normalized * maxSpeed;
		}
	}

	void moveTo()
	{
		getNextLocation();
	}

	void move()
	{
		if (run == false)
		{
			if (this.transform.position.x - endPos.x < 1f && this.transform.position.x - endPos.x > -1f && this.transform.position.y - endPos.y < 1f && this.transform.position.y - endPos.y > -1f)
			{
				moveTo();
				velocity = endPos - this.transform.position;
			}
			Invoke(movementType, 0f);
		}
		else
			runningScript();

		myRB.velocity = Vector3.zero;
		myRB.velocity = velocity;
	}

	void runningScript()
	{
		Vector3 temp;
		Vector2 positionPoint = Vector2.zero;
		//Vector3 check = Vector3.zero;

		float tempStore;

		positionPoint = this.myRB.velocity;

		curTime -= Time.deltaTime;
		if (curTime <= 0)
		{
			velocity = this.transform.position - playerRef.GetComponent<Rigidbody2D>().transform.position;
			velocity = velocity.normalized * maxSpeed;
			curTime = MAXTIME;
		}

		Debug.DrawRay(this.transform.position, positionPoint, Color.blue);
		hit = Physics2D.Raycast(this.transform.position, positionPoint, positionPoint.normalized.magnitude, layermask);
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.tag.Equals("building"))
			{
				// Add direction from hit face, times how much force to repel by

				temp = positionPoint;
				tempStore = temp.x;
				temp.x = temp.y * -1.0f;
				temp.y = tempStore;
				velocity += temp * 5f;
				velocity = velocity.normalized * maxSpeed;
			}
		}
	}

	void death()
	{
		test.SendMessage("modScore", 100);
		Debug.Log("here");
		playerRef.GetComponent<TransformAbilities_scr>().SendMessage("allowChange", true);
		Destroy(this.gameObject);
	}


	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "Player")
		{
			//Debug.Log("hitting");
			distance = coll.GetComponent<Rigidbody2D>().transform.position - this.transform.position;
			actualDistance = distance.magnitude;
			//Physics2D.IgnoreLayerCollision(8, 9, false);
		}
		if (coll.tag == "Player" && coll.GetComponent<TransformAbilities_scr>().attack == true && actualDistance <= 1.0f)
		{
			//Debug.Log ("collided");
			myRB.velocity = coll.GetComponent<movement_scr>().returnVelocity;
			alive = false;
			//death ();
		}
	}

	void setConfined(bool var)
	{
		//confinedTo = var;
	}

	void setEndPos(Vector3 var)
	{
		endPos = var;
	}

	void setArea(GameObject[] var)
	{
		myArea = var;
	}

	void getNextLocation()
	{
		if (confinedTo == true)
			setEndPos(myArea[Random.Range(0, myArea.Length)].transform.position);
		else
		{
			myLocation = (myLocation + 1) % myArea.Length;
			setEndPos(myArea[myLocation].transform.position);
		}
	}

	void currentLocation(int var)
	{
		myLocation = var;
	}

	void runChange()
	{
		//Debug.Log("happened");
		if (run != true)
		{
			run = true;
			Physics2D.IgnoreLayerCollision(8, 10, false);
		}
	}

	void setMovement(string inputSetting)
	{
		movementType = inputSetting;
	}

	void normal()
	{
	}
	void staticMove()
	{
	}
}
