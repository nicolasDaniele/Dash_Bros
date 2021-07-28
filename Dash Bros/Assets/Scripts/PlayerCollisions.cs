using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{

    // This script must manage plater's collisions with enemies
    // If dashing, player kills the enemy, else, enemy kills the player

    private Dash dash;
    private PlayerMove pm;
    private Animator anim;
    [SerializeField]
    private GameObject[] items;
    private int collectedCount;

    public GameObject game;
    public GameObject collected;
    public GameObject panel;
    public Canvas scoreCanvas;
   
    // Start is called before the first frame update
    void Start()
    {
        dash = GetComponent<Dash>();
        pm = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        items = GameObject.FindGameObjectsWithTag("Item");
        collectedCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (dash.isDashing)
            {
                collision.gameObject.GetComponent<Enemy>().Die();
                anim.SetBool("isJumping", false);
                anim.SetBool("isDashing", false);
                anim.SetBool("isWalking", false);
            }
            else
            {
                Die();
            }
        }

        if (collision.gameObject.CompareTag("Item") || collision.gameObject.CompareTag("Prize"))
        {
            Vector3 collectedPos = collision.gameObject.transform.position;
            ItemScore(collectedPos);
            Destroy(collision.gameObject);
            Instantiate(collected, collectedPos, Quaternion.identity);
            if(collision.gameObject.CompareTag("Item"))
                collectedCount++;
            CheckItems();
        }
    }

    void ItemScore(Vector3 _pos)
    {
        Vector3 textPos = Camera.main.WorldToScreenPoint(_pos);
        GameObject pan = Instantiate(panel);
        pan.transform.position = textPos;
        pan.transform.SetParent(scoreCanvas.transform);
        Destroy(pan, 1f);
        ScoreManager.instance.AddScore(200);
    }

    void Die()
    {
        game.GetComponent<GameController>().gs = GameController.GameStates.END;
        anim.SetTrigger("die");
        dash.enabled = false;
        pm.enabled = false;
        Destroy(gameObject, 1f);
    }

    void CheckItems()
    {
        if(collectedCount >= items.Length)
            game.GetComponent<GameController>().gs = GameController.GameStates.WIN;
    }
}
