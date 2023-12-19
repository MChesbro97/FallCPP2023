using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    AudioSourceManager asm;

    public AudioClip deathSound;
    protected int health;
    public int maxHealth;

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        asm = GetComponent<AudioSourceManager>();

        if (maxHealth <= 0)
            maxHealth = 10;

        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Null check for asm
            if (asm != null)
            {
                asm.PlayOneShot(deathSound, false);
            }
            else
            {
                Debug.LogError("AudioSourceManager is null on Enemy!");
                // Handle the error, e.g., by disabling sound-related functionality.
            }
            anim.SetTrigger("Death");
            Destroy(gameObject, 2);

        }
            
    }
}
