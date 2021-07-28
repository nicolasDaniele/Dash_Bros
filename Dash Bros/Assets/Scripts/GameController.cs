using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelPause;

    public enum GameStates {PLAY, END, WIN, PAUSE, TRANSITION};
    public GameStates gs;

    // Start is called before the first frame update
    void Start()
    {
        panelWin.SetActive(false);
        panelPause.SetActive(false);
        gs = GameStates.PLAY;
    }

    // Update is called once per frame
    void Update()
    {
        if (gs == GameStates.WIN)
        {
            panelWin.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gs == GameStates.PAUSE)
                gs = GameStates.PLAY;
            else
                gs = GameStates.PAUSE;
        }

        if (gs == GameStates.PAUSE)
        {
            Time.timeScale = 0;
            panelPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            panelPause.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
