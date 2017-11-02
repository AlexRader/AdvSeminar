using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLvl_scr : MonoBehaviour 
{
	private Vector3 playerLocation = new Vector3 (-25.0f, 0.0f);
	[SerializeField]
	private GameObject level;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject AI;

	[SerializeField]
	private GameObject savedVars;
	// Use this for initialization
	void Start () 
	{
		Instantiate (level, Vector3.zero, Quaternion.identity);
		Instantiate (player, playerLocation, Quaternion.identity);
		Instantiate (savedVars, Vector3.zero, Quaternion.identity);
		Instantiate (AI, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
