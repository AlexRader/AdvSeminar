using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class optionSelection_scr : MonoBehaviour 
{
	//private Canvas menu;
	[SerializeField]
	private int currentButton;
    [SerializeField]
    private TextMeshProUGUI[] selectors;

    public GameObject selectorItem;
	public GameObject controls;
	private GameObject toDestroy;
	private GameObject storedVariables;
    Vector3 wolfStart = new Vector3(-5.5f, -1f, 0.0f);
    Vector3 wolfSecond = new Vector3(0.5f, -1f, 0.0f);
	Vector3 wolfThird = new Vector3(6f, -1f, 0.0f);

	private bool created = false;

	// Use this for initialization
	void Start () {
        selectors = GetComponentsInChildren<TextMeshProUGUI>();
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
		if (Input.GetKeyDown( KeyCode.A) || Input.GetKeyDown( KeyCode.LeftArrow))
			currentButton -= 1;
		else if (Input.GetKeyDown( KeyCode.D) || (Input.GetKeyDown( KeyCode.RightArrow)))
		{
			currentButton += 1;
		}
		withinBounds();

		if (Input.GetKeyDown( KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			selectCase();
		}
        if (currentButton == 0)
        {
            selectorItem.transform.position = wolfStart;
        }
        else if(currentButton == 1)
        {
            selectorItem.transform.position = wolfSecond;
        }
		else
			selectorItem.transform.position = wolfThird;
	}

	void withinBounds()
	{
		if (currentButton < 0)
		{
			currentButton = selectors.Length - 1;
		}
		if (currentButton >= selectors.Length)
		{
			currentButton = currentButton % selectors.Length;
		}
	}
	void selectCase()
	{
		switch (currentButton)
		{
			case(0):
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				break;
			case (1):
				createControls();
				break;
			case (2):
				Application.Quit();
				break;
		}
	}
	void createControls()
	{
		created = !created;

		if (!created)
		{
			Destroy(toDestroy);
		}
		else
		{
			Instantiate(controls);
			toDestroy = GameObject.FindGameObjectWithTag("controls");
		}

	}
}
