using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRotation_scr : MonoBehaviour {

	private float yRotation;
    private const float INC = .1f; 

	// Use this for initialization
	void Start ()
	{
			yRotation = -90f;
	}

    void rotate()
	{
        yRotation += Time.deltaTime + INC;

        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
	}
}
