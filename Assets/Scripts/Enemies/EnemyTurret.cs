using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class EnemyTurret : Enemy
{
    Shoot shootScript;
    AudioSourceManager asm;

    public AudioClip shootSound;

    public float projectileFireRate;
    //public Transform player;

    float timeSinceLastFire = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        shootScript = GetComponent<Shoot>();
        asm = GetComponent<AudioSourceManager>();

        if (shootScript == null)
            Debug.Log("Shoot script not added");
        if (asm == null)
            Debug.Log("ASM not added");

        if (projectileFireRate <= 0)
            projectileFireRate = 2;

        shootScript.OnProjectileSpawned += PlayShootSound;
    }

    void PlayShootSound()
    {
        asm.PlayOneShot(shootSound, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerInstance != null)
        {
            // Check the distance between the turret and the player
            float distanceToPlayer = Vector2.Distance(transform.position, GameManager.Instance.playerInstance.transform.position);

            // Set a threshold distance at which the turret starts shooting
            float shootingDistance = 7f; // You can adjust this value to your desired distance

            if (distanceToPlayer <= shootingDistance)
            {
                FlipTurret();

                AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

                if (curPlayingClips[0].clip.name != "Fire")
                {
                    if (Time.time >= timeSinceLastFire + projectileFireRate)
                    {
                        anim.SetTrigger("Fire");
                        timeSinceLastFire = Time.time;
                    }
                }
            }
        }
    }


    void FlipTurret()
    {
        // Flip the turret based on the player's position
        if (GameManager.Instance.playerInstance.transform.position.x > transform.position.x)
        {
            // Player is on the right, flip the turret
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            // Player is on the left, flip the turret
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
