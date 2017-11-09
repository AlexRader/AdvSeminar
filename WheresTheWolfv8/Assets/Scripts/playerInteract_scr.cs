using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract_scr : MonoBehaviour {

	private bool isActive;
	//public bool spotted = false;
	public bool anotherSpotted = false;
	Vector3 var1, var2;

	public LayerMask layermask;

	RaycastHit2D hit;
	// Use this for initialization
	void Start () {
		isActive = true;
		Physics2D.IgnoreLayerCollision(9, 8, false);
		}
	
	// Update is called once per frame
	void Update () {
	}


	void changeInteraction(bool var)
	{
		isActive = var;
		if (isActive == true)
			Physics2D.IgnoreLayerCollision(9, 8, false);
		else
			Physics2D.IgnoreLayerCollision(9, 8, true);
	}

	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (isActive == true && coll.tag == "npc")
		{
			var1 = this.GetComponent<Rigidbody2D>().transform.position;
			var2 = coll.GetComponent<Rigidbody2D>().transform.position;
			var2 = var2 - var1;
			//if ()
			hit = Physics2D.Raycast(var1, var2, 64, layermask);

			Debug.DrawRay(var1, var2, Color.green);
			if(hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "npc")
				{
					coll.SendMessage("runChange");
				}
			}

		}
	}
}
