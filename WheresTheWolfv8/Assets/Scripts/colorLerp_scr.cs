using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorLerp_scr : MonoBehaviour
{

    public Color collideColor = new Color(0.9f, 0.1f, 0.1f, 0.5f);
    public Color normalColor = new Color(1.0f, 1.0f, 1.0f, 1f);

    void startRoutine()
    {
        StartCoroutine(Flasher()); //VERY IMPORTANT!  You 'must' start coroutines with this code.
    }

    // Functions to be used as Coroutines MUST return an IEnumerator
    IEnumerator Flasher()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = collideColor;
            //renderer.material.color = collideColor;
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<SpriteRenderer>().color = normalColor;
            //renderer.material.color = normalColor;
            yield return new WaitForSeconds(.1f);
        }
    }
}
