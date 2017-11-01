using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class aiControl_scr : MonoBehaviour {

	//private GameObject[] npcControl;
	//private List<GameObject> aiList;
	public GameObject[] firstMarket;
	public GameObject[] secondMarket;

	public GameObject[] homeArea;
	public GameObject[] patrolArea;
	//private List<int> maxCapacity;
	//private List<List<GameObject>> locations;

	//public GameObject myPrefab;

	public GameObject mySpawn;

	private GameObject newObj;
	private Vector3 spawnPos;

	private int theCount = 0;


	public GameObject[] findObjects;

	private int[] standardSpawns;

	//private int inc = 0;

	private float multiplyFactor = 1.0f;

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
			standardSpawns = new int[4] { 8, 10, 4, 15 };
			break;
		case TypeDay.Fishing:
			standardSpawns = new int[4] { 10, 12, 4, 15 };
			break;
		case TypeDay.Festival:
				standardSpawns = new int[4] { 10, 10, 6, 18 };
				break;
		case TypeDay.Sunday:
				standardSpawns = new int[4] { 12, 14, 5, 10 };
				break;
		}
		for (int i = 0; i < standardSpawns.Length; ++i) 
		{
			standardSpawns [i] = Mathf.RoundToInt(standardSpawns [i] * multiplyFactor);
//			Debug.Log (standardSpawns [i]);
		}
		setLists ();

		//aiList = new List<GameObject> ();


		spawnUnits ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
		

	}

	void areaOfSpawn (int var)
	{
		switch (var) 
		{
		case 0:
			spawnPos = firstMarket [Random.Range (0, (firstMarket.Length))].transform.position;
			newObj = Instantiate (mySpawn, spawnPos, Quaternion.identity);
			newObj.SendMessage ("setConfined", true);
			//newObj.SendMessage ("setMyObject", this.gameObject);
			newObj.SendMessage("setArea", firstMarket);

			newObj.name = "AI" + theCount;
			//GameObject.Find (newObj.name).SendMessage ("setValues", var);
			break;
		case 1:
			spawnPos = secondMarket [Random.Range (0, (secondMarket.Length))].transform.position;
			newObj = Instantiate (mySpawn, spawnPos, Quaternion.identity);
			newObj.SendMessage ("setConfined", true);
			//newObj.SendMessage ("setMyObject", this.gameObject);
			newObj.SendMessage("setArea", secondMarket);

			newObj.name = "AI" + theCount;
			break;
		case 2:
			spawnPos = homeArea[Random.Range(0, (homeArea.Length))].transform.position;
			newObj = Instantiate(mySpawn, spawnPos, Quaternion.identity);
			newObj.SendMessage("setConfined", true);
			//newObj.SendMessage("setMyObject", this.gameObject);
			newObj.SendMessage("setArea", homeArea);

			newObj.name = "AI" + theCount;
			break;
		case 3:
			int location = Random.Range(0, (patrolArea.Length));
			spawnPos = patrolArea[location].transform.position;
			newObj = Instantiate(mySpawn, spawnPos, Quaternion.identity);
			newObj.SendMessage("currentLocation", location);
			newObj.SendMessage("setConfined", false);
			//newObj.SendMessage("setMyObject", this.gameObject);
			newObj.SendMessage("setArea", patrolArea);


			newObj.name = "AI" + theCount;
			break;

		}
	}
	/*
	Vector2 getListPosition(int var)
	{
		
	}
*/
	void setLists()
	{
		//if (firstMarket == null) 
		//{			
			//firstMarket = GameObject.FindGameObjectsWithTag ("firstMarket");
			//setArray("firstMarket");
			//for (int i = 0; i < findObjects.Length; ++i) 
			//{
		firstMarket = GameObject.FindGameObjectsWithTag ("firstMarket");
		secondMarket = GameObject.FindGameObjectsWithTag ("secondMarket");
		homeArea = GameObject.FindGameObjectsWithTag("homeLocation");
		patrolArea = GameObject.FindGameObjectsWithTag("walkPath");
	//		Debug.Log (firstMarket.Length);
	//GameObject.Find ("npc_prefab").SendMessage ("moveTo");
	//}
	//addFactors(firstMarket);

		//}


	}

	// no longer used
	/*
	void returnNextMove(GameObject sentObj)
	{
		
		switch (sentObj.GetComponent<aiMovement_scr>().confinedTo)
		{
		case 0:
			returnVec = firstMarket [Random.Range (0, (firstMarket.Length))].transform.position;
			break;
		case 1:
			returnVec = secondMarket [Random.Range (0, (secondMarket.Length))].transform.position;
			break;
		case 2:
			returnVec = homeArea[Random.Range(0, (homeArea.Length))].transform.position;
			break;
		}
		sentObj.SendMessage ("setEndPos", returnVec);
	}*/
	/*
	void addFactors(List<GameObject> var)
	{
		//locations [numLists] = var;
		locations [numLists] = var;
	}
*/
	/*
	void setArray(string var)
	{
		if (findObjects != null)
			Array.Clear (findObjects, 0, findObjects.Length);
		findObjects = GameObject.FindGameObjectsWithTag (var);
	}*/
	/*
	Vector3 returnListLocation(int var, int var2)
	{
		locations
	}
*/
}
