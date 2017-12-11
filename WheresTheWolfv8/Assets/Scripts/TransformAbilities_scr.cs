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

	private float maxDash = .7f;
	private float maxBite = 1.0f;

	private float cooldownTimer = 3f;

	public Vector2 savedVelocity;

	private Rigidbody2D rb;

	public bool attack = false;

	private bool changeBack = true;

	private GameObject cameraRef;
	private bool bite = false;

    private Animator anim;

    public bool checkDash = true;

    public GameObject dashParticles;

    private Vector3 selfieSpawner;

    public GameObject selfieCollider;

    private GameObject selfieRef;
    private bool selfieSpawned = false;

    public enum DashState 
	{
		Ready = 0,
		Dashing = 1,
		Cooldown = 2
	}
	public enum BiteState
	{
		Ready = 0,
		Biting = 1,
        Cooldown = 2
	}

	void Biting()
	{
		switch (biteState) 
		{
		case BiteState.Ready:
			var isBiteKeyDown = Input.GetKeyDown (KeyCode.J);
			if (isBiteKeyDown && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hack-n-Slash")) 
			{
				savedVelocity = rb.velocity;
				attack = true;
				rb.velocity = Vector2.zero;
				biteState = BiteState.Biting;
				gameObject.GetComponent<CircleCollider2D>().radius = 1f;
				SendMessage("slashingNow", attack);
               }
			break;
		case BiteState.Biting:
			biteTimer -= Time.deltaTime;
			if (biteTimer <= 0) 
			{
				attack = false;
				biteTimer = maxBite + 1f;
				rb.velocity = savedVelocity;
				gameObject.GetComponent<CircleCollider2D>().radius = 3f;
				biteState = BiteState.Cooldown;
                SendMessage("slashingNow", attack);
            }
            break;
        case BiteState.Cooldown:
            {
                biteTimer -= Time.deltaTime;
                if (biteTimer <= 0)
                {
                    biteTimer = maxBite;
                    biteState = BiteState.Ready;
                    attack = false;
                }
                break;
            }
		}
	}

	void Dashing()
	{
		switch (dashState) 
		{
		case DashState.Ready:
			var isDashKeyDown = Input.GetKeyDown (KeyCode.K);
            checkDash = true;
			if(isDashKeyDown && !anim.GetCurrentAnimatorStateInfo(0).IsName("BasicAttack2"))
			{
                Instantiate(dashParticles, gameObject.transform.position, gameObject.transform.rotation);
                checkDash = false;
				savedVelocity = rb.velocity;
				attack = true;
                GameObject.FindGameObjectWithTag("Player").SendMessage("changeMaxVel", 20f);
                rb.velocity = new Vector2(rb.velocity.x * 5f, rb.velocity.y * 5f);
                dashState = DashState.Dashing;
                SendMessage("dashingNow", attack);
				gameObject.GetComponent<CircleCollider2D>().radius = 1f;
			}
			break;
		case DashState.Dashing:
			dashTimer -= Time.deltaTime;
			if(dashTimer <= 0)
			{
                GameObject.FindGameObjectWithTag("Player").SendMessage("changeMaxVel", -20f);
                attack = false;
				dashTimer = cooldownTimer;
				rb.velocity = savedVelocity;
				dashState = DashState.Cooldown;
				gameObject.GetComponent<CircleCollider2D>().radius = 3f;
				SendMessage("dashingNow", attack);
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

    void attackModify()
    {
		bool shakeType;
		if (bite)
			shakeType = false;
		else
			shakeType = true;
        cameraRef.SendMessage("shakingRoutine", shakeType);
    }

	// Use this for initialization
	void Start () 
	{
		werewolfForm = false;
		changeForm();
		dashTimer = maxDash;
		rb = GetComponent<Rigidbody2D> ();
		cameraRef = GameObject.FindGameObjectWithTag("MainCamera");
        anim = gameObject.GetComponent<Animator>();
        selfieSpawner = new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
            selfieSpawner = new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y, gameObject.transform.position.z);
        else
            selfieSpawner = new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y, gameObject.transform.position.z);
        if (selfieSpawned == true)
        {
            selfieRef.SendMessage("changePos", selfieSpawner);
            Debug.Log(selfieRef.name);
        }
    }

    void werewolfInputs()
    {
        if (werewolfForm == true && !anim.GetCurrentAnimatorStateInfo(0).IsName("Transition"))
        {
            Dashing();
            Biting();
        }
        newInputs();
    }

	void newInputs()
	{
		if (Input.GetKeyDown (KeyCode.L) && changeBack == true)
		{
            if (notAttacking())
            {
                Instantiate(selfieCollider, selfieSpawner, Quaternion.identity);
                changeForm();
                allowChange(true);
            }
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
            GetComponent<Timer_scr>().SendMessage("reset");
            SendMessage("changeVisual", werewolfForm);
            checkDash = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").SendMessage("changeMaxVel", -10f);
            GetComponent<Timer_scr>().SendMessage("clear");
            biteTimer = 0;
            dashTimer = 0;
            Dashing();
            Biting();
            SendMessage("changeVisual", werewolfForm);
            checkDash = false;
        }
        GameObject.FindGameObjectWithTag("variables").GetComponent<savedVariables_scr>().SendMessage("setBool");
        GameObject.FindGameObjectWithTag("Player").SendMessage("changeInteraction", werewolfForm);
    }
    
    private bool notAttacking()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hack-n-Slash")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("BasicAttack2")
            )
            return false;
        else
            return true;
    }

    void despawned()
    {
        selfieSpawned = false;
        selfieRef = null;
    }
    void spawned()
    {
        selfieSpawned = true;
        selfieRef = GameObject.FindGameObjectWithTag("theSelfie");
        Debug.Log(selfieRef.name);
    }

}
