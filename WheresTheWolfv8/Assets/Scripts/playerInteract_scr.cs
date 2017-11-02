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
		isActive = false;
		Physics2D.IgnoreLayerCollision(9, 8, true);
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
		/*
		 hit = Physics2D.Raycast(this.transform.position, positionPoint, positionPoint.magnitude, layermask);
		if(hit.collider != null)
		{
			//Debug.Log ("preMod" + velocity);
			//if (hit.transform == transform)
				//Debug.Log ("fuck");
			//Debug.Log (hit.collider.tag);
			// Make sure ray is not hitting itself
			if(hit.collider.gameObject.tag.Equals("building")) 
			{
					//Debug.Log("EnemyScript.FindAIInput: RayHit on " + hit.collider.gameObject.tag);

					// Add direction from hit face, times how much force to repel by

					temp = positionPoint;
					tempStore = temp.x;
					temp.x = temp.y * -1.0f;
					temp.y = tempStore;
//					Debug.Log ("preMod" + velocity);
					velocity += temp * 15f;
//					Debug.Log ("postMod" + velocity);
			}
		}
		 */
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
					//anotherSpotted = Physics2D.Linecast(var1, var2, 1 << LayerMask.NameToLayer("Buildings"));
					//Debug.Log(anotherSpotted);
					//Debug.Log(hit.collider.gameObject.tag);
					coll.SendMessage("runChange");
				}
			}

		}


		//spotted = false;
		
	}
}
