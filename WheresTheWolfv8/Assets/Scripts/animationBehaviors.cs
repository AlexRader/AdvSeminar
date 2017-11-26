using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationBehaviors : MonoBehaviour
{
    public Animator anim;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animationChecks();
        spriteDirection();
    }

    void spriteDirection()
    {
        if (rb.velocity.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (rb.velocity.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    void animationChecks()
    {
        if (rb.velocity.SqrMagnitude() != 0 && anim.GetBool("attacking") == false)
        {
            anim.SetTrigger("movement");
        }
        else if (rb.velocity.SqrMagnitude() == 0 && anim.GetBool("attacking") == false)
        {
            anim.SetTrigger("idle");
        }
    }

    void dashingNow(bool isAttacking)
    {
        anim.SetTrigger("charge");
        anim.SetBool("attacking", isAttacking);
    }

    void slashingNow(bool isAttacking)
    {
        anim.SetTrigger("slash");
        anim.SetBool("attacking", isAttacking);
    }

    void changeVisual(bool currentForm)
    {
        anim.SetBool("werewolf", currentForm);
    }
}
