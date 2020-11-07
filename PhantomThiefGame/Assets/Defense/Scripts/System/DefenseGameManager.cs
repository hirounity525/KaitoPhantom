using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private DefenseInputProvider inputProvider;
    [SerializeField] private DefenseFriendHPControler friendHPControlller;

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

                inputProvider.canInput = true;

                if(friendHPControlller.hitPoints <= 0)
                {
                    //gameState = GameState.GAMEOVER;
                }

                break;
            case GameState.PAUSE:

                break;
            case GameState.GAMEOVER:

                inputProvider.canInput = false;

                break;
            case GameState.CLEAR:

                break;
        }
    }
}
