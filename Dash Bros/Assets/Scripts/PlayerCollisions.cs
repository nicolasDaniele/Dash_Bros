using System;
using UnityEngine;


public class PlayerCollisions : MonoBehaviour
{
    public static Action onPlayerDestroyed;

    private Dash dash;
    private PlayerMove playerMove;
    private Animator anim;
    [SerializeField]
    private GameObject[] items;
    private int collectedCount;

    public GameObject collected;
    public GameObject panel;
    public Canvas scoreCanvas;
   
    // Start is called before the first frame update
    void Start()
    {
        dash = GetComponent<Dash>();
        playerMove = GetComponent<PlayerMove>();
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Pit"))
        {
            Die();
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
        GameController.instance.gameState = GameController.GameStates.END;
        anim.SetTrigger("die");
        dash.enabled = false;
        playerMove.enabled = false;
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
        onPlayerDestroyed.Invoke();
    }

    void EnablePlayer()
    {
        gameObject.SetActive(true);
    }

    void CheckItems()
    {
        if(collectedCount >= items.Length)
            GameController.instance.gameState = GameController.GameStates.WIN;
    }
}
