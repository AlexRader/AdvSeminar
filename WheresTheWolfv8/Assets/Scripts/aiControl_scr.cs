using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class aiControl_scr : MonoBehaviour {

	public GameObject[] patrolArea;
	public GameObject[] staticPeople;

    public GameObject[] aiSpawn;

    public AnimatorController[] allAnimation;

    public GameObject mySpawn;

	private GameObject newObj;
	private Vector3 spawnPos;

	private int theCount = 0;

    private int enemyCount = 0;
    private int sacrificeCount = 0;
    private const int MAX_ENEMIES = 20;
    private const int MAX_SACRIFICES = 40;
    private int currentEnemies = 10;
    private int extraEnemies = 0;

    private float spawnTimer;
    private const float MAX_SPAWN_TIMER = .5f;

    bool switchDirection = false;

    public GameObject[] findObjects;

	private int[] standardSpawns;
	private int numberOfItems;

	private float multiplyFactor = 1.0f;

	int randomSpawn = 0;

	public DayState dayState;
	public TypeDay typeDay;
	public enum DayState
	{
		Clear = 0,
		Rain = 1,
		Hot = 2,
		Cloudy = 3,
		Windy = 4
	}

	public enum TypeDay
	{
		Standard = 0,
		Fishing = 1,
		Festival = 2,
		Sunday = 3,
	}

	// Use this for initialization
	void Start () 
	{
		dayState = (DayState)Random.Range (0, 5);
		typeDay = (TypeDay)Random.Range (0, 4);
        spawnTimer = MAX_SPAWN_TIMER;

		switch (dayState) 
		{
		case DayState.Clear:
			multiplyFactor = 1.2f;
			break;
		case DayState.Rain:
			multiplyFactor = .5f;
			break;
		case DayState.Hot:
			multiplyFactor = .75f;
			break;
		case DayState.Cloudy:
			multiplyFactor = 1.3f;
			break;
		case DayState.Windy:
			multiplyFactor = .9f;
			break;

		}

		switch (typeDay) 
		{
		case TypeDay.Standard:
			standardSpawns = new int[1] { 50 };
			break;
		case TypeDay.Fishing:
			standardSpawns = new int[1] { 52 };
			break;
		case TypeDay.Festival:
				standardSpawns = new int[1] { 65 };
				break;
		case TypeDay.Sunday:
				standardSpawns = new int[1] { 55 };
				break;
		}
		for (int i = 0; i < standardSpawns.Length; ++i) 
		{
			standardSpawns [i] = Mathf.RoundToInt(standardSpawns [i] * multiplyFactor);
		}
		setLists ();

		spawnUnits ();
	}

    private void Update()
    {
        extraEnemies = savedVariables_scr.score / 100;
        if (extraEnemies > MAX_ENEMIES)
            extraEnemies = MAX_ENEMIES;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            if (enemyCount < currentEnemies + extraEnemies)
            {
                condensedSpawnPacket();

                newObj.SendMessage("switchEnemy", true);
                newObj.SendMessage("setAnimator", allAnimation[5]);
                newObj.SendMessage("setReturn");
                enemyCount++;
            }
            if (sacrificeCount < MAX_SACRIFICES)
            {
                condensedSpawnPacket();

                newObj.SendMessage("switchEnemy", false);
                newObj.SendMessage("setAnimator", allAnimation[Random.Range(1, 5)]);
                newObj.SendMessage("setReturn");
                sacrificeCount++;
            }
            spawnTimer = MAX_SPAWN_TIMER;
        }
        switchDirection = !switchDirection;
    }

    void condensedSpawnPacket()
    {
        int location = Random.Range(0, (aiSpawn.Length));
        if ((aiSpawn[location].transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude < 100)
        {
            if (location == 0)
                location = aiSpawn.Length - 1;
            else if (location == aiSpawn.Length - 1)
                location = 0;
            else
                location += 1;
        }
        spawnPos = aiSpawn[location].transform.position;
        newObj = Instantiate(mySpawn, spawnPos, Quaternion.identity);
        newObj.SendMessage("currentLocation", location);
        newObj.SendMessage("setConfined", switchDirection);
        newObj.SendMessage("setMoveable", false);
        newObj.SendMessage("setArea", patrolArea);

        newObj.name = "AI" + theCount;
    }


    void enemyAISpawn(int var)
    {
        enemyCount--;
    }
    void aiSpawns(int var)
    {
        sacrificeCount--;
    }

    void spawnInfo(Vector3 var, bool fooConfined, bool barConfined)
    {
        newObj = Instantiate(mySpawn, var, Quaternion.identity);
        newObj.SendMessage("setConfined", fooConfined);
        newObj.SendMessage("setMoveable", barConfined);
    }

	void spawnUnits()
	{
		for (int i = 0; i < standardSpawns.Length; i++) 
		{
			for (int j = 0; j < standardSpawns [i]; j++) 
			{
				theCount++;
				areaOfSpawn ();
			}
		}
		
		for (int i = 0; i < staticPeople.Length; i++)
		{
			spawnPos = staticPeople [i].transform.position;
            spawnInfo(spawnPos, false, true);
            newObj.SendMessage("setAnimator", allAnimation[0]);
            newObj.SendMessage("setArea", staticPeople);
		}
	}

	void areaOfSpawn()
	{
		randomSpawn = Random.Range(0, 10);

        int location = Random.Range(0, (patrolArea.Length));
		spawnPos = patrolArea[location].transform.position;
		newObj = Instantiate(mySpawn, spawnPos, Quaternion.identity);
		newObj.SendMessage("currentLocation", location);
		newObj.SendMessage("setConfined", switchDirection);
		newObj.SendMessage("setMoveable", false);
		newObj.SendMessage("setArea", patrolArea);

		newObj.name = "AI" + theCount;

        if (randomSpawn >= 8)
        {
            newObj.SendMessage("switchEnemy", true);
            newObj.SendMessage("setAnimator", allAnimation[5]);
            enemyCount++;
        }
        else
        {
            newObj.SendMessage("switchEnemy", false);
            newObj.SendMessage("setAnimator", allAnimation[Random.Range(1, 5)]);
            sacrificeCount++;
        }
        switchDirection = !switchDirection;
	}

	void setLists()
	{
		numberOfItems = GameObject.FindGameObjectsWithTag ("walkPath").Length;
		patrolArea = GameObject.FindGameObjectsWithTag ("walkPath");
		for (int i = 0; i < numberOfItems; i++) 
		{
			patrolArea [i] = GameObject.Find ("walkPath_prefab" + i);
		}
		staticPeople = GameObject.FindGameObjectsWithTag ("notMoving");

        aiSpawn = GameObject.FindGameObjectsWithTag("spawnPos");
	}

}
