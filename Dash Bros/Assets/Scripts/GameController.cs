using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameStates {PLAY, END};
    public GameStates gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameStates.PLAY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
