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
    public GameController game;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player.transform.position = new Vector3(playerX, playerY, player.transform.position.z);
        player.GetComponentInChildren<Rigidbody2D>().isKinematic = true;

        StartCoroutine(StartLevel());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(game.GetComponent<GameController>().gs);
        tileMap.transform.position = new Vector3(tileMap.transform.position.x,
                                                 gridY,
                                                 tileMap.transform.position.z);

       // if (game.GetComponent<GameController>().gs == GameController.GameStates.TRANSITION)
            player.transform.position = new Vector3(playerX, playerY, player.transform.position.z);

        if (game.GetComponent<GameController>().gs == GameController.GameStates.WIN)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                game.GetComponent<GameController>().gs = GameController.GameStates.TRANSITION;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    IEnumerator StartLevel()
    {
        game.GetComponent<GameController>().gs = GameController.GameStates.TRANSITION;
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LeavePlayer());
    }

    IEnumerator LeavePlayer()
    {
        anim.SetTrigger("LeavePlayer");
        yield return new WaitForSeconds(0);
        player.GetComponentInChildren<Rigidbody2D>().isKinematic = false;
        game.GetComponent<GameController>().gs = GameController.GameStates.PLAY;


    }
}
