using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject tileMap;
    public GameObject player;
    public float gridY = 13;
    public float playerX = 0;
    public float playerY = 0;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player.transform.position = new Vector3(playerX, playerY, player.transform.position.z);

        if(player.GetComponentInChildren<Rigidbody2D>())
            player.GetComponentInChildren<Rigidbody2D>().isKinematic = true;
        if(player.GetComponentInChildren<PlayerMove>())
           player.GetComponentInChildren<PlayerMove>().enabled = false;

        StartCoroutine(StartLevel());
    }

    // Update is called once per frame
    void Update()
    {
        tileMap.transform.position = new Vector3(tileMap.transform.position.x,
                                                 gridY,
                                                 tileMap.transform.position.z);

            player.transform.position = new Vector3(playerX, playerY, player.transform.position.z);

        if (GameController.instance.gameState == GameController.GameStates.WIN)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameController.instance.gameState = GameController.GameStates.TRANSITION;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    IEnumerator StartLevel()
    {
        GameController.instance.gameState = GameController.GameStates.TRANSITION;
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LeavePlayer());
    }

    IEnumerator LeavePlayer()
    {
        anim.SetTrigger("LeavePlayer");
        yield return new WaitForSeconds(0);

        if(player.GetComponentInChildren<Rigidbody2D>())
            player.GetComponentInChildren<Rigidbody2D>().isKinematic = false;
        if(player.GetComponentInChildren<PlayerMove>())
           player.GetComponentInChildren<PlayerMove>().enabled = true;
            
        GameController.instance.gameState = GameController.GameStates.PLAY;
    }
}
