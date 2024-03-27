using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelPause;

    public enum GameStates {PLAY, END, WIN, PAUSE, TRANSITION};
    public GameStates gameState;

    public static GameController instance { get; private set; }

    private void Awake() 
    {    
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        panelWin.SetActive(false);
        panelPause.SetActive(false);
        gameState = GameStates.PLAY;
    }

    private void OnEnable() 
    {
        PlayerCollisions.onPlayerDestroyed += ReturnToTitleScreen;    
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameStates.WIN)
        {
            panelWin.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameStates.PAUSE)
                gameState = GameStates.PLAY;
            else
                gameState = GameStates.PAUSE;
        }

        if (gameState == GameStates.PAUSE)
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

    void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
