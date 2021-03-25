using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ShootingGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private ShootingInputProvider inputProvider;
    [SerializeField] private SoundManager soundManager;

    [Header("Start")]
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string playBlockName;
    [SerializeField] private TimelineController startTimeline;

    [Header("Main")]
    [SerializeField] private ShootingEnemyManager enemyManager;
    [SerializeField] private ShootingPlayerHPControler playerHPControler;

    [Header("Pause")]
    [SerializeField] private PauseController pauseController;

    [Header("Clear")]
    [SerializeField] private TimelineController clearTimeline;


    private SneakingCameraChanger cameraChanger;
    private SceneLoader sceneLoader;
    private CommonDataController commonDataController;

    private bool isFirstStatePlay;
    private bool isStartTimeline;

    private bool isFirstMainState;

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
                    enemyManager.DisableEnemies();

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
                    pauseController.canPause = true;

                    if (!isFirstMainState)
                    {
                        enemyManager.EnableEnemies();
                        isFirstMainState = true;
                    }

                    soundManager.Play("ShootingBGM");

                    isFirstStatePlay = true;
                }

                if (playerHPControler.playerNowHitPoint <= 0)
                {
                    gameState = GameState.GAMEOVER;
                    return;
                }

                if (enemyManager.IsAllDestroy())
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
                    inputProvider.canInput = false;

                    soundManager.Pause();

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
