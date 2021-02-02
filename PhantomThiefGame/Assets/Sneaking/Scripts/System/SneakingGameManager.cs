using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SneakingGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private SneakingInputProvider inputProvider;

    [Header("Start")]
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string playBlockName;
    [SerializeField] private TimelineController startTimeline;

    [Header("Main")]
    [SerializeField] private SneakingEnemyManager enemyManager;

    private bool isFirstStatePlay;
    private bool isStartTimeline;

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

                if (!isFirstStatePlay)
                {
                    inputProvider.canInput = false;

                    flowchart.SendFungusMessage(playBlockName);

                    isFirstStatePlay = true;
                    return;
                }

                if (flowchart.GetBooleanVariable("isFinish"))
                {
                    if (!isStartTimeline)
                    {
                        startTimeline.Play();
                        isStartTimeline = true;
                    }
                    else
                    {
                        if (startTimeline.isFinish)
                        {
                            isFirstStatePlay = false;
                            isStartTimeline = false;

                            gameState = GameState.MAIN;
                        }
                    }
                }

                break;
            case GameState.MAIN:
                //インプット使えるようにする
                inputProvider.canInput = true;

                //ポーズボタンを押したら、ポーズに移動する

                //プレイヤーが見つかったら、ゲームオーバーに移動する
                if (enemyManager.isPlayerDiscovery)
                {
                    gameState = GameState.GAMEOVER;
                }
                //プレイヤーがゴールしたら、クリアに移動する

                break;
            case GameState.PAUSE:
                inputProvider.canInput = true;

                break;
            case GameState.GAMEOVER:
                inputProvider.canInput = false;
                break;
            case GameState.CLEAR:
                inputProvider.canInput = false;
                break;
        }
    }
}
