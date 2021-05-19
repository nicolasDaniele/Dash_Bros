using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    // This script must manage plater's collisions with enemies
    // If dashing, player kills the enemy, else, enemy kills the player

    private Dash dash;
    private PlayerMove pm;
    private Animator anim;

    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        dash = GetComponent<Dash>();
        pm = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (dash.isDashing)
            {
                collision.gameObject.GetComponent<Enemy>().Die();
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        game.GetComponent<GameController>().gs = GameController.GameStates.END;
        anim.SetTrigger("die");
        dash.enabled = false;
        pm.enabled = false;
        Debug.Log("Player died!");
        Destroy(gameObject, 1f);
    }
}
