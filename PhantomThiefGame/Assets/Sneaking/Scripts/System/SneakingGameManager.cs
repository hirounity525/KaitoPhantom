using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SneakingGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private SneakingInputProvider inputProvider;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SEPlayer sEPlayer;

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

    [Header("Pause")]
    [SerializeField] private PauseController pauseController;

    [Header("Clear")]
    [SerializeField] private TimelineController clearTimeline;


    private SneakingCameraChanger cameraChanger;
    private SceneLoader sceneLoader;
    private CommonDataController commonDataController;

    private bool isFirstStatePlay;
    private bool isStartTimeline;

    private bool isFirstDiscoveryState;

    private void Awake()
    {
        cameraChanger = GetComponent<SneakingCameraChanger>();
        sceneLoader = GetComponent<SceneLoader>();
        commonDataController = GetComponent<CommonDataController>();
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
                    soundManager.Play("SneakingBGM");

                    inputProvider.canInput = true;
                    pauseController.canPause = true;

                    isFirstStatePlay = true;
                }

                if (enemyManager.isPlayerDiscovery)
                {
                    if (!isFirstDiscoveryState)
                    {
                        sEPlayer.Play("見つかる");

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

                    return;
                }

                if (playerManager.IsClear())
                {
                    gameState = GameState.CLEAR;
                    return;
                }

                if (pauseController.isPause)
                {
                    isFirstStatePlay = false;
                    gameState = GameState.PAUSE;
                }

                break;
            case GameState.PAUSE:

                if (!isFirstStatePlay)
                {
                    soundManager.Pause();
                    inputProvider.canInput = false;

                    isFirstStatePlay = true;
                }

                if (!pauseController.isPause)
                {
                    isFirstStatePlay = false;
                    gameState = GameState.MAIN;
                }

                break;
            case GameState.GAMEOVER:

                inputProvider.canInput = false;

                sceneLoader.LoadScene("GameOver");

                break;
            case GameState.CLEAR:

                inputProvider.canInput = false;

                if (!isStartTimeline)
                {
                    clearTimeline.Play();
                    isStartTimeline = true;
                }
                else
                {
                    if (clearTimeline.isFinish)
                    {
                        isFirstStatePlay = false;
                        isStartTimeline = false;

                        commonDataController.ClearStage();
                        sceneLoader.LoadScene("Title");
                    }
                }

                break;
        }
    }
}
