using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    private int score = 0;
    private int highscore = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Restart highscore
        //PlayerPrefs.SetInt("highscore", 0);

        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();

        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);

        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = highscore.ToString();
    }
}
