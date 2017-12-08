using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnLvl_scr : MonoBehaviour 
{
	Scene scene;
	private Vector3 playerLocation = new Vector3 (6.0f, 21.5f);
	private Vector3 cameraDepth = new Vector3 (-25.0f, 0.0f, -1.0f);
	private Vector3 lightPosition = new Vector3(-0f, 30f, -12f);
	[SerializeField]
	private GameObject level;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject AI;
	[SerializeField]
	private GameObject savedVars;
	[SerializeField]
	private GameObject myCamera;
	[SerializeField]
	private GameObject directionalLight;
	// Use this for initialization
	void Start () 
	{
		scene = SceneManager.GetActiveScene();
		Instantiate (level, Vector3.zero, Quaternion.identity, transform);
		Instantiate (savedVars, Vector3.zero, Quaternion.identity, transform);
		Instantiate (AI, Vector3.zero, Quaternion.identity, transform);
		Instantiate (myCamera, cameraDepth, Quaternion.identity, transform);
		Instantiate (player, playerLocation, Quaternion.identity, transform);
		if (scene.name == "Main Scene")
			directionalLight = Instantiate(directionalLight, lightPosition, Quaternion.Euler(0,-90,0), transform);
		else
			directionalLight = Instantiate(directionalLight, lightPosition, Quaternion.Euler(0,90,0), transform);
	}

    private void Update()
    {
        directionalLight.SendMessage("rotate");
	}
}
