using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_scr : MonoBehaviour {

	private float curAmount;
	private float maxAmount;
	private Vector2 vecAmount;

    public AudioClip hurtSound;
    public AudioSource mySource;
    // Use this for initialization
    void Start () 
	{
        mySource = GetComponent<AudioSource>();
        maxAmount = 10.0f;
		curAmount = maxAmount;
		vecAmount = new Vector2(curAmount, maxAmount);
	}
	
	void modHP(float damage)
	{	
        if (damage > 0)
        {
            mySource.clip = hurtSound;
            mySource.Play();
        }
		vecAmount.x -= damage;
        if (vecAmount.x > maxAmount)
            vecAmount.x = maxAmount;
        
		this.SendMessage ("HandleBar", vecAmount);
	}

	void health()
	{
        GameObject.Find("ControlObject").SendMessage("gameOver");
    }
}
