using UnityEngine;

public class BonusMarker_scr : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    void setActive(bool set)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = set;
    }
}
