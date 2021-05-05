using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float startDashTime;
    private float dashTime;
    private int direction;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        direction = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb2d.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    rb2d.velocity += Vector2.left * dashSpeed * Time.deltaTime;
                }
                else if (direction == 2)
                {
                    rb2d.velocity += Vector2.right * dashSpeed * Time.deltaTime;
                }
            }
        }

        Debug.Log(dashTime);
    }
}
