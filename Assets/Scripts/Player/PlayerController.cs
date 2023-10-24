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
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        float attack = Input.GetAxisRaw("Fire1");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if(hInput > 0) 
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if(hInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetFloat("vInput", Mathf.Abs(vInput));
        anim.SetFloat("attack", attack);
    }
}
    