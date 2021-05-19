using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed = 2.5f;
    [SerializeField]
    private float XoffSet = -0.1f;
    private float minMarginX;
    private float maxMarginX;
    
    private Animator anim;
    private Animator parentAnim;

    public GameObject parent;
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        parentAnim = parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // This is kind of an offset in x axis so that the enemy doesn't reach
        // the same value in x as the player,
        // Otherwise he goes like berzerk
        if (player != null)
        {
            minMarginX = player.transform.position.x - XoffSet;
            maxMarginX = player.transform.position.x + XoffSet;
        }
        
        if (game.GetComponent<GameController>().gs == GameController.GameStates.PLAY)
        {
            // Enemy hunts the player
            if (transform.position.x < maxMarginX)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("isMoving", true);
            }
            else if (transform.position.x > minMarginX)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("isMoving", true);
            }
            else
            {
                transform.Translate(Vector2.zero);
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            transform.Translate(Vector2.zero);
            anim.SetBool("isMoving", false);
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        parentAnim.SetTrigger("die");
        Destroy(gameObject, 0.5f);
    }
}

