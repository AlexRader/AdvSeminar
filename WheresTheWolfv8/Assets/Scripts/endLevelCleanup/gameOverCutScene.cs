using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverCutScene : MonoBehaviour
{
    private float duration = 1.8f;
    private Color temp;
    private bool happened = false;
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
        temp = GetComponent<SpriteRenderer>().color;
        temp.a = 0.0f;
    }

    // Update is called once per frame
    void Update ()
    {
        lerpAlpha();
	}

    void lerpAlpha()
    {
        if (temp.a < .85f)
        {
            temp.a += Time.deltaTime / duration;
            GetComponent<SpriteRenderer>().color = temp;
        }
        else if (temp.a >= .85f && !happened)
        {
            canvas.SetActive(true);
        }
    }

}
