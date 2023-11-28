using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{
    Rigidbody2D rb;
    public float xSpeed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xSpeed <= 0)
            xSpeed = 2;
    }
    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(transform.parent.gameObject, 2);

        if (damage == 9999)
        {
            anim.SetTrigger("Squish");
            return;
        }

        base.TakeDamage(damage);

        Debug.Log("Enemy Walker took damage " + damage.ToString() + " damage");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name == "Walk")
        {
            rb.velocity = sr.flipX ? new Vector2(-xSpeed, rb.velocity.y) : new Vector2(xSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }
}
