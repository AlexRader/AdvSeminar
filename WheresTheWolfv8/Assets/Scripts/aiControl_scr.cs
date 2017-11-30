using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
//using System;

public class aiControl_scr : MonoBehaviour {

	public GameObject[] firstMarket;
	public GameObject[] secondMarket;

	public GameObject[] homeArea;
	public GameObject[] patrolArea;
	public GameObject[] staticPeople;

    public AnimatorController[] allAnimation;

    public GameObject mySpawn;

	private GameObject newObj;
	private Vector3 spawnPos;

	private int theCount = 0;


	public GameObject[] findObjects;

	private int[] standardSpawns;
	private int numberOfItems;

	//private int inc = 0;

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
		//maxCapacity = new List<int> ();
		//locations = new List<List<GameObject>> ();
//		Debug.Log(dayState);
//		Debug.Log(typeDay);

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
				areaOfSpawn (i);
			}
		}
		
		for (int i = 0; i < staticPeople.Length; i++)
		{
			spawnPos = staticPeople [i].transform.position;
            spawnInfo(spawnPos, true, true);
            newObj.SendMessage("setAnimator", allAnimation[0]);
            newObj.SendMessage("setArea", staticPeople);
		}
	}

	void areaOfSpawn(int var)
	{
		randomSpawn = Random.Range(0, 10);
		/*switch (var) 
		{
		case 0:
			spawnPos = firstMarket [Random.Range (0, (firstMarket.Length))].transform.position;
            spawnInfo(spawnPos, true, false);
			newObj.SendMessage("setArea", firstMarket);

			newObj.name = "AI" + theCount;
			break;
		case 1:
			spawnPos = secondMarket [Random.Range (0, (secondMarket.Length))].transform.position;
            spawnInfo(spawnPos, true, false);
            newObj.SendMessage("setArea", secondMarket);

			newObj.name = "AI" + theCount;
			break;
		case 2:
			spawnPos = homeArea[Random.Range(0, (homeArea.Length))].transform.position;
            spawnInfo(spawnPos, true, false);
            newObj.SendMessage("setArea", homeArea);

			newObj.name = "AI" + theCount;
			break;
		case 3:
			int location = Random.Range(0, (patrolArea.Length));
			spawnPos = patrolArea[location].transform.position;
			newObj = Instantiate(mySpawn, spawnPos, Quaternion.identity);
			newObj.SendMessage("currentLocation", location);
			newObj.SendMessage("setConfined", false);
			newObj.SendMessage ("setMoveable", false);
            newObj.SendMessage("setArea", patrolArea);


			newObj.name = "AI" + theCount;
			break;

		}*/
		switch (var)
		{
			case 0:
				int location = Random.Range(0, (patrolArea.Length));
				spawnPos = patrolArea[location].transform.position;
				newObj = Instantiate(mySpawn, spawnPos, Quaternion.identity);
				newObj.SendMessage("currentLocation", location);
				newObj.SendMessage("setConfined", false);
				newObj.SendMessage("setMoveable", false);
				newObj.SendMessage("setArea", patrolArea);


				newObj.name = "AI" + theCount;
				break;
		}
        if (randomSpawn >= 8)
        {
            newObj.SendMessage("switchEnemy", true);
            newObj.SendMessage("setAnimator", allAnimation[5]);
        }
        else
        {
            newObj.SendMessage("switchEnemy", false);
            newObj.SendMessage("setAnimator", allAnimation[Random.Range(1, 5)]);
        }
	}

	void setLists()
	{
		//firstMarket = GameObject.FindGameObjectsWithTag ("firstMarket");
		//secondMarket = GameObject.FindGameObjectsWithTag ("secondMarket");
		//homeArea = GameObject.FindGameObjectsWithTag("homeLocation");
		//patrolArea = GameObject.FindGameObjectsWithTag ("walkPath");
		numberOfItems = GameObject.FindGameObjectsWithTag ("walkPath").Length;
		patrolArea = GameObject.FindGameObjectsWithTag ("walkPath");
		for (int i = 0; i < numberOfItems; i++) 
		{
			patrolArea [i] = GameObject.Find ("walkPath_prefab" + i);
		}
		staticPeople = GameObject.FindGameObjectsWithTag ("notMoving");
	}

}
