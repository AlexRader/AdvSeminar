using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lightRotation_scr : MonoBehaviour {

	Scene scene;
	private float yRotation;

	// Use this for initialization
	void Start ()
	{
		scene = SceneManager.GetActiveScene();
		if (scene.name == "Main Scene")
			yRotation = -90f;
		else
			yRotation = 90f;
	}

	void rotate(float currentRotation)
	{
		gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation + currentRotation, transform.eulerAngles.z);
	}
}
