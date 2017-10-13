using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class aiControl_scr : MonoBehaviour {

	//private GameObject[] npcControl;
	//private List<GameObject> aiList;
	public GameObject[] firstMarket;
	public GameObject[] secondMarket;
	//private List<int> maxCapacity;
	//private List<List<GameObject>> locations;

	//public GameObject myPrefab;

	public GameObject mySpawn;

	private GameObject newObj;
	private Vector3 spawnPos;
	private Vector3 returnVec;

	private int theCount = 0;

	private bool theTime;

	public GameObject[] findObjects;

	private int[] standardSpawns;

	private int numLists = 0;

	//private int inc = 0;

	private float multiplyFactor = 1.0f;

	public DayState dayState;
	public TypeDay typeDay;
	public enum DayState
	{
		Clear = 0,
		Rain = 1
	}

	public enum TypeDay
	{
		Standard = 0,
		Fishing = 1
	}



	// Use this for initialization
	void Start () 
	{
		dayState = (DayState)Random.Range (0, 2);
		typeDay = (TypeDay)Random.Range (0, 2);
		//maxCapacity = new List<int> ();
		//locations = new List<List<GameObject>> ();
		Debug.Log(dayState);
		Debug.Log(typeDay);

		switch (dayState) 
		{
		case DayState.Clear:
			multiplyFactor = 1.2f;
			break;
		case DayState.Rain:
			multiplyFactor = .5f;
			break;
		}

		switch (typeDay) 
		{
		case TypeDay.Standard:
			standardSpawns = new int[2] { 8, 10 };
			break;
		case TypeDay.Fishing:
			standardSpawns = new int[2] {10, 12};
			break;
		}
		for (int i = 0; i < standardSpawns.Length; ++i) 
		{
			standardSpawns [i] = Mathf.RoundToInt(standardSpawns [i] * multiplyFactor);
			Debug.Log (standardSpawns [i]);
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
			newObj.SendMessage ("setConfined", var);
			newObj.SendMessage ("setMyObject", this.gameObject);
			newObj.name = "AI" + theCount;
			//GameObject.Find (newObj.name).SendMessage ("setValues", var);
			break;
		case 1:
			spawnPos = secondMarket [Random.Range (0, (secondMarket.Length))].transform.position;
			newObj = Instantiate (mySpawn, spawnPos, Quaternion.identity);
			newObj.SendMessage ("setConfined", var);
			newObj.SendMessage ("setMyObject", this.gameObject);
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
	//		Debug.Log (firstMarket.Length);
		//GameObject.Find ("npc_prefab").SendMessage ("moveTo");
			//}
			//addFactors(firstMarket);

		//}
		

	}

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
		}
		sentObj.SendMessage ("setEndPos", returnVec);
	}
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
