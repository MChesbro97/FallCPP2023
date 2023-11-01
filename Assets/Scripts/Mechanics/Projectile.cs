using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    public float lifetime;
    


    //This is meant to be modfied by the object creating the projectile
    //eg. the shoot script
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector2 moveDirection = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
       
        if (lifetime <= 0) lifetime = 2.0f;
        Destroy(gameObject, lifetime);
         
    }
    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent < Rigidbody2D>();
        rb.velocity = moveDirection.normalized * speed;
    }

}
