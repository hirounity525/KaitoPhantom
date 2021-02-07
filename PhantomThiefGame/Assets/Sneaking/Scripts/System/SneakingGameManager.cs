using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

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
    [SerializeField] private SneakingPlayerManager playerManager;
    [SerializeField] private SneakingUIManager uIManager;
    [SerializeField] private float lifeDeleteWaitTime;
    [SerializeField] private float fadeInTime;


    private SneakingCameraChanger cameraChanger;

    private bool isFirstStatePlay;
    private bool isStartTimeline;

    private bool isFirstDiscoveryState;

    private void Awake()
    {
        cameraChanger = GetComponent<SneakingCameraChanger>();
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

                if (!isFirstStatePlay)
                {
                    inputProvider.canInput = true;

                    isFirstStatePlay = true;
                }

                if (enemyManager.isPlayerDiscovery)
                {
                    if (!isFirstDiscoveryState)
                    {
                        inputProvider.canInput = false;

                        playerManager.AddDamage();
                        cameraChanger.ChangeMainCamera(enemyManager.discoverdLihgtVC);

                        uIManager.StartLifeDelete(lifeDeleteWaitTime);

                        isFirstDiscoveryState = true;
                    }

                    if (uIManager.isDeleteLife)
                    {
                        if (playerManager.NowHP() > 0)
                        {
                            cameraChanger.ChangeMainCamera(enemyManager.discoverdLihgtVC);
                            uIManager.FadeOutPanel(fadeInTime);

                            playerManager.ResetPlayer();
                            enemyManager.ResetEnemies();
                        }
                        else
                        {
                            gameState = GameState.GAMEOVER;
                        }

                        isFirstDiscoveryState = false;
                        enemyManager.isPlayerDiscovery = false;
                        uIManager.isDeleteLife = false;

                        isFirstStatePlay = false;
                    }
                }

                break;
            case GameState.PAUSE:
                inputProvider.canInput = true;

                break;
            case GameState.GAMEOVER:
                inputProvider.canInput = false;

                SceneManager.LoadScene("GameOver");

                break;
            case GameState.CLEAR:
                inputProvider.canInput = false;
                break;
        }
    }
}
