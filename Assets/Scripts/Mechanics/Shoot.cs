using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    public float projectileSpeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (projectileSpeed <= 0) projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Please set default values on " + gameObject.name);
    }

    /*public void fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = projectileSpeed;
        }
    }*/

    public void fire()
    {
        
        Projectile curProjectile;

        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.moveDirection = Vector2.right;
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.moveDirection = Vector2.left;
        }

        
        curProjectile.speed = projectileSpeed;

        
       
    }
}
