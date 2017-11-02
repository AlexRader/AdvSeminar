﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class optionSelection_scr : MonoBehaviour 
{
	//private Canvas menu;
	[SerializeField]
	private int currentButton;
	[SerializeField]
	private Button[] buttons;

	private GameObject storedVariables;
	// Use this for initialization
	void Start () {
		//menu = GameObject.Find("CanvasLookup").GetComponent<Canvas>();
		buttons = this.GetComponentsInChildren<Button>();
		currentButton = 0;

		storedVariables = GameObject.FindGameObjectWithTag("variables");
		storedVariables.SendMessage("clearStored");
		storedVariables.SendMessage("setBool");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown( KeyCode.A) || (Input.GetKeyDown( KeyCode.LeftArrow)))
			currentButton -= 1;
		else if (Input.GetKeyDown( KeyCode.D) || (Input.GetKeyDown( KeyCode.RightArrow)))
		{
			currentButton += 1;
		}
		withinBounds();
		buttons[currentButton].Select();

		if (Input.GetKeyDown( KeyCode.Return))
		{
			selectCase();
		}
	}

	void withinBounds()
	{
		if (currentButton < 0)
		{
			currentButton = buttons.Length - 1;
		}
		if (currentButton >= buttons.Length)
		{
			currentButton = currentButton % buttons.Length;
		}
	}
	void selectCase()
	{
		switch (currentButton)
		{
			case(0):
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				break;
			case(1):
				Application.Quit();
				break;
		}
	}
}
