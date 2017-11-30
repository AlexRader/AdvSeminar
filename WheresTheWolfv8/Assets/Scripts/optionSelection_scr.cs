using System.Collections;
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

    public GameObject selectorItem;

	private GameObject storedVariables;
    Vector3 wolfStart = new Vector3(-1.0f, -1f, 0.0f);
    Vector3 wolfSecond = new Vector3(1.5f, -1f, 0.0f);
    //Vector3 wolfStart = new Vector3(0.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start () {
		//menu = GameObject.Find("CanvasLookup").GetComponent<Canvas>();
		buttons = this.GetComponentsInChildren<Button>();
		currentButton = 0;

		storedVariables = GameObject.FindGameObjectWithTag("variables");
		storedVariables.SendMessage("clearStored");
		storedVariables.SendMessage("setBool");

        // = blah;
        selectorItem.transform.position = wolfStart;

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
        if (currentButton == 0)
        {
            selectorItem.transform.position = wolfStart;
        }
        else
        {
            selectorItem.transform.position = wolfSecond;
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
