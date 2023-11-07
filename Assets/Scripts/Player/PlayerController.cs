using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    Coroutine jumpForceChange;
    Coroutine speedChange;


    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log("Score has ben set to: " + _score.ToString());
        }
    }

    private int _lives = 3;

    public int lives
    {
        get => _lives;
        set
        {
            //if (_lives > value)
            //Respawn = lost a life

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

            //if (_lives < 0)
            //gameover

            Debug.Log("Lives has ben set to: " + _lives.ToString());
        }
    }
    public int maxLives = 5;

    //Movement Variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    //Groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        //Getting our component references
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //checking variables for dirty data
        if(rb == null)
            Debug.Log("No Rigidbody Reference");
        if(sr == null)
            Debug.Log("No SpriteRenderer Reference");
        if (anim == null)
            Debug.Log("No Animator Reference");

        if(groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("Groundcheck set to default value");
        }

        if(speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed set to default value");
        }
        if(jumpForce <= 0)
        {
            jumpForce = 300.0f;
            Debug.Log("JumpForce set to default value");
        }

        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        float attack = Input.GetAxisRaw("Fire1");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        if (isGrounded) rb.gravityScale = 1;
        
        if (curPlayingClips.Length > 0) 
        {
            if (curPlayingClips[0].clip.name == "Attack")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        {

        
        }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
        }

        if (hInput != 0) sr.flipX = (hInput < 0);

  

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetFloat("vInput", Mathf.Abs(vInput));
        
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null) jumpForceChange = StartCoroutine(JumpForceChange());
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }

    }

    public void StartSpeedChange()
    {
        if (speedChange == null) speedChange = StartCoroutine(SpeedChange());
        else
        {
            StopCoroutine(speedChange);
            speedChange = null;
            speed /= 2;
            speedChange = StartCoroutine(SpeedChange());
        }

    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
        jumpForceChange = null;
    }

    IEnumerator SpeedChange()
    {
        speed *= 2;
        yield return new WaitForSeconds(5.0f);
        speed /= 2;
        speedChange = null;
    }
    //called the frame that the collision happened
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    //called the frame that the collision exits
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    //called on the second frame while in the collision, and continuously called while you remain in the collider
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }


}
    