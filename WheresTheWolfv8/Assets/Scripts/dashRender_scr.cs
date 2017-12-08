using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashRender_scr : MonoBehaviour
{
    private SpriteRenderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.parent.gameObject.GetComponent<TransformAbilities_scr>().checkDash)
            rend.enabled = true;
        else
            rend.enabled = false;
		
	}
}
