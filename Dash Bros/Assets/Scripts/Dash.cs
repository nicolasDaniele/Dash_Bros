using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private float dashSpeed = 500f;
    [SerializeField]
    private float startDashTime = 0.2f;
    private float dashTime;
    private int direction;
    private bool dash;
    public bool isDashing = false;


    private Rigidbody2D rb2d;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashTime = startDashTime;
        direction = 0;
    }

    // Update is called once per frame    
    private void FixedUpdate()
    {
        //if (dash)
        //{
            if (direction == 0)
            {
                if (dash && transform.localScale.x < 0)
                {
                    direction = 1;
                    isDashing = true;
                }
                else if (dash && transform.localScale.x > 0)
                {
                    direction = 2;
                    isDashing = true;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    isDashing = false;
                    direction = 0;
                    dashTime = startDashTime;
                    rb2d.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    if (direction == 1)
                    {
                        rb2d.velocity += Vector2.left * dashSpeed;
                        transform.localScale = new Vector3(-transform.localScale.x,
                                                           transform.localScale.y,
                                                           transform.localScale.z);
                    }
                    else if (direction == 2)
                    {
                        rb2d.velocity += Vector2.right * dashSpeed;
                        transform.localScale = new Vector3(transform.localScale.x,
                                                           transform.localScale.y,
                                                           transform.localScale.z);
                    }
                }
            //}
            dash = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }
        anim.SetBool("isDashing", isDashing);
    }
}
