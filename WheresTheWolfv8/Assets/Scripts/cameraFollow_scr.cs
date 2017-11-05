using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow_scr : MonoBehaviour {

	private Vector2 velocity;
	public float smoothTimeX;
	public float smoothTimeY;
	private float posX;
	private float posY;

	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate()
	{
		posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		transform.position = new Vector3(posX, posY, transform.position.z);
	}


}
