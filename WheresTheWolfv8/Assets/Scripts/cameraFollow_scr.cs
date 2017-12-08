using System.Collections;
using UnityEngine;

public class cameraFollow_scr : MonoBehaviour
{

	//the following is for the follow camera function
	private Vector2 velocity;
	public float smoothTimeX;
	public float smoothTimeY;
	private float posX;
	private float posY;

	private GameObject player;

	//camera shake function vars
	private bool shaking = false;
	private bool typeShake = false;
	private float shakeAmount;
	private float timer;
	private const float MAXTIME = 1.6f;

	// Use this for initialization
	void Start()
	{
		timer = MAXTIME;
		player = GameObject.FindGameObjectWithTag("Player");
		shakeAmount = 4f;
	}

	void FixedUpdate()
	{
		posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		transform.position = new Vector3(posX, posY, transform.position.z);
	}

	private void Update()
	{
		if (shaking)
		{
			timer -= Time.deltaTime;
			if (timer > 0)
			{
				Vector3 newPos = transform.position + Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
				if (typeShake == false)
					newPos.y = transform.position.y;
				newPos.z = transform.position.z;

				transform.position = newPos;
			}
			else
			{
				shaking = false;
				timer = MAXTIME;
			}
		}
	}

	public void shakingRoutine(bool type)
	{
		shaking = true;
		typeShake = type;
	}
}
