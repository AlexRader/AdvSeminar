using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfie_scr : MonoBehaviour {

    private GameObject playerRef;
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(1);
        playerRef.GetComponent<TransformAbilities_scr>().SendMessage("despawned");
        Destroy(gameObject);
    }
	// Use this for initialization
	void Start ()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerRef.GetComponent<TransformAbilities_scr>().SendMessage("spawned");
        StartCoroutine("despawn");
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "npc")
        {
            coll.SendMessage("SelfieTaken", -.3f);
        }
    }

    void changePos(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
}
