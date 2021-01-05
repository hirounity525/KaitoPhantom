using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.START:
                break;
            case GameState.MAIN:
                break;
            case GameState.PAUSE:
                break;
            case GameState.GAMEOVER:
                break;
            case GameState.CLEAR:
                break;
        }
    }
}
