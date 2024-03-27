using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float horizMove;
    [SerializeField]
    private float horizSpeed = 300f;
    [SerializeField]
    private float jumpForce = 200f;
    private bool isWalking = false;
    private bool jump = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Horizontal move
        horizMove = Input.GetAxis("Horizontal") * horizSpeed * Time.deltaTime;
        rb2d.velocity = new Vector2(horizMove, rb2d.velocity.y);

        Jump();       
    }

    private void Update()
    {
        // Flip avatar & set walk animation true or false
        if (horizMove < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            isWalking = true;
        }
        else if (horizMove > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
        // Jump
        if (Input.GetAxis("Vertical") > 0 && GroundCheck.isGrounded)
            jump = true;
        // Jump animation
        anim.SetBool("isJumping", !GroundCheck.isGrounded);
        // Walk animation
        anim.SetBool("isWalking", isWalking);
    }    

    void Jump()
    {
        if (jump)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            jump = false;
        }      
    }
}
