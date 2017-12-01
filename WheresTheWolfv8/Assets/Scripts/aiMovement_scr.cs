﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

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
    private float storedSpeed;

    private bool alive;

    private bool deathHappened = false;

    private float modVelocity;


    private Vector3 velocity;

    private Vector3 distance;

    public bool confinedTo;
    private bool notMoveable;

    public GameObject[] myArea;
    public GameObject shot;

    private int myLocation = 0;

    private bool run = false;
    private bool returnNow = false;
    RaycastHit2D hit;
    int layermask = 1 << 10;
    int layerMaskTwo = 1 << 9;

    private GameObject playerRef;
    public GameObject[] aiGenPath;
    private int numberOfItems;

    private Component test;

    private const float MAXTIME = 1.0f;
    private float curTime = 0;
	private float moveCheck = 0;
    private float attackTime;
    private bool enemyAI;
    public Color color;

    private Animator anim;

    float waitNumber;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        endPos = this.transform.position;
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(8, 0, true);
        Physics2D.IgnoreLayerCollision(8, 11, true);
        alive = true;
        modVelocity = (Random.Range(MIN_SPEED_MODIFY, MAX_SPEED_MODIFY)) / 20.0f;

        maxSpeed = maxSpeed + modVelocity + modVelocity + modVelocity;
        storedSpeed = maxSpeed;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        test = GameObject.FindGameObjectWithTag("variables").GetComponent<savedVariables_scr>();

        numberOfItems = GameObject.FindGameObjectsWithTag("walkPath").Length;
        aiGenPath = GameObject.FindGameObjectWithTag("ai_control").GetComponent<aiControl_scr>().patrolArea;

        attackTime = MAXTIME;

        anim = gameObject.GetComponent<Animator>();
    }

    void spriteDirection()
    {
        if (myRB.velocity.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (myRB.velocity.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    void animationChecks()
    {
        if (myRB.velocity.SqrMagnitude() > .3f)
        {
            anim.SetBool("movement", true);
        }
        else if (myRB.velocity.SqrMagnitude() <= .3f)
        {
            anim.SetBool("movement", false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            move();
            spriteDirection();
            animationChecks();
        }
        else if (deathHappened == false || myRB.velocity.magnitude > 0)
        {
            anim.SetBool("dead", true);
            deathHappened = !deathHappened;
            //deathtimer -= Time.deltaTime;
            myRB.velocity = myRB.velocity / DRAG;
            if (myRB.velocity.magnitude <= STOPSPEED)
                myRB.velocity = Vector2.zero;
            if (myRB.velocity == Vector2.zero)
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
    void waitTill()
    {
    }
    // to get the pause function working so the environment is more believable use
    // setMoveable(true);
    void move()
    {
        if (run == false && notMoveable == false && returnNow == false)
        {
            if (this.transform.position.x - endPos.x < 1f && this.transform.position.x - endPos.x > -1f)
            {
                if (this.transform.position.y - endPos.y < 1f && this.transform.position.y - endPos.y > -1f)
                {
                    moveTo();
                    velocity = endPos - this.transform.position;
                }
            }
        }
        else if (run == false && returnNow == true)
        {
			moveCheck -= Time.deltaTime;
            if (moveCheck <= 0)
            {
                velocity = endPos - this.transform.position;
                moveCheck = MAXTIME;
            }
            if (this.transform.position.x - endPos.x < 1f && this.transform.position.x - endPos.x > -1f)
            {
                if (this.transform.position.y - endPos.y < 1f && this.transform.position.y - endPos.y > -1f)
                {
                    returnNow = false;
                    setConfined(false);
                    moveTo();
                    velocity = endPos - this.transform.position;
                }
            }
						rayCastDetection();
        }
        else if (run == true)
            runningScript();
        //velocity = this.transform.position - GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().transform.position;

        //velocity /= inc;
        myRB.velocity = Vector3.zero;
        myRB.velocity = velocity;
    }

    void runningScript()
    {
        Vector3 temp;
        Vector2 positionPoint = Vector2.zero;
        Vector2 playerDirection = Vector2.zero;

        Vector2 position;
        float distance;
        bool attacking = false;
        //Vector3 check = Vector3.zero;

        float tempStore;

        positionPoint = this.myRB.velocity;

        playerDirection = playerRef.transform.position - transform.position;

        curTime -= Time.deltaTime;
        if (enemyAI == false)
        {
            //anim.SetTrigger("running");
            if (curTime <= 0)
            {
                velocity = this.transform.position - playerRef.GetComponent<Rigidbody2D>().transform.position;
                velocity = velocity.normalized * maxSpeed;
                curTime = MAXTIME;
            }
        }
        else
        {
            anim.SetTrigger("running");
            if (curTime <= 0)
            {
                position = playerRef.GetComponent<Rigidbody2D>().transform.position - this.transform.position;
                distance = position.magnitude;
                if (distance <= 6.0f)
                {
                    attacking = true;
                }
                if (distance > 6.0f && attacking == false)
                {
                    velocity = playerRef.GetComponent<Rigidbody2D>().transform.position - this.transform.position;
                    velocity = velocity.normalized * maxSpeed;
                    curTime = MAXTIME;
                    anim.SetBool("aim", false);
                }
                else
                {
                    velocity *= 0;
                    attackingNow();
                }
            }
        }

        Debug.DrawRay(this.transform.position, playerDirection, Color.red);
        hit = Physics2D.Raycast(this.transform.position, playerDirection, (playerDirection.normalized.magnitude) * .5f, layerMaskTwo);

        if (playerRef.GetComponent<playerInteract_scr>().isActive == false)
        {
            if (enemyAI)
            {
                anim.SetBool("aim", false);
                anim.SetBool("shoot", false);
            }
            if (enemyAI != true)
                anim.SetBool("run", false);

            returnNow = true;
            run = false;
            setMoveable(false);
            setConfined(false);
            findEndpoint();
            maxSpeed = storedSpeed;
            setArea(aiGenPath);

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
                velocity += temp * 2f;
                velocity = velocity.normalized * maxSpeed;
            }
        }
    }

    void rayCastDetection()
    {
        Vector3 temp;
        float tempStore;
        Vector2 positionPoint = myRB.velocity;

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
                velocity += temp * 1.5f;
                velocity = velocity.normalized * maxSpeed;
            }
        }
    }


    void death()
    {
        test.SendMessage("modScore", 100);
		playerRef.GetComponent<TransformAbilities_scr>().SendMessage("allowChange", true);
        gameObject.layer = 11;
        //Destroy (this.gameObject);
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && coll.GetComponent<TransformAbilities_scr>().attack == true)
        {
			playerRef.GetComponent<TransformAbilities_scr>().SendMessage("attackModify");
			myRB.velocity = coll.GetComponent<movement_scr>().returnVelocity;
            alive = false;
        }
    }

    void setConfined(bool var)
    {
        confinedTo = var;
    }

    void setMoveable(bool var)
    {
        notMoveable = var;
    }

    void setAnimator(AnimatorController var)
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.runtimeAnimatorController = var;
    }


    // useless function
    void setMyObject(GameObject var)
    {
        //myObject = var;
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
            returnNow = false;
            Physics2D.IgnoreLayerCollision(8, 10, false);
            if (enemyAI)
            {
                maxSpeed = 10;
            }
            else
                anim.SetBool("run", true);
        }
    }

    void switchEnemy(bool var)
    {
        enemyAI = var;
    }

    void attackingNow()
    {
        attackTime -= Time.deltaTime;
        anim.SetBool("aim", true);
        anim.SetBool("shoot", false);
        float var = playerRef.GetComponent<Rigidbody2D>().transform.position.x - transform.position.x;
        if (var >= 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        if (attackTime <= 0)
        {
            anim.SetBool("shoot", true);
            attackTime = MAXTIME;
            Instantiate(shot, gameObject.transform.position, Quaternion.identity);
        }
    }

    void findEndpoint()
    {
        Vector3 tempPosition = Vector3.zero, comparePosition = Vector3.zero;
        int i = 0;

        comparePosition = aiGenPath[i].transform.position;
        //comparePosition = comparePosition - gameObject.transform.position;
        tempPosition = comparePosition;

        for (i = 0;  i < numberOfItems; i++)
        {
            comparePosition = aiGenPath[i].transform.position;
            //comparePosition = comparePosition - gameObject.transform.position;
            if ((comparePosition - gameObject.transform.position).magnitude <= (tempPosition - gameObject.transform.position).magnitude)
            {
                tempPosition = comparePosition;
                currentLocation(i);
            }
        }
        
        myArea = aiGenPath;
        endPos = tempPosition;
    }
}
