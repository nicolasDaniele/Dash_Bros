using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float horizMove; // For horizontal movement
    [SerializeField]
    private float horizSpeed = 300f; // For horizontal speed
    [SerializeField]
    private float jumpForce = 200f;
    private bool isGrounded;
    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Flip avatar
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        // Jump animation
        anim.SetBool("isJumping", !isGrounded);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizMove = Input.GetAxis("Horizontal") * horizSpeed * Time.deltaTime;
        rb2d.velocity = new Vector2(horizMove, rb2d.velocity.y);
        // Walk animation
        anim.SetFloat("walking", Mathf.Abs(horizMove));

        // Horizontal move
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }


    }

    // Ground check
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}