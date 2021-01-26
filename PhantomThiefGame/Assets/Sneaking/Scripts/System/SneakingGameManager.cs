using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private TimelineController startTimeline;

    private bool isFirstStatePlay;

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
                //インプット使えるようにする
                //ポーズボタンを押したら、ポーズに移動する
                //プレイヤーが見つかったら、ゲームオーバーに移動する
                //プレイヤーがゴールしたら、クリアに移動する
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
