using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class RunGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private RunInputProvider inputProvider;

    [Header("Start")]
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string playBlockName;
    [SerializeField] private TimelineController startTimeline;

    [Header("Main")]
    [SerializeField] private RunPlayerHPController playerHPController;
    [SerializeField] private RunObjectManager objectManager;

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
                    inputProvider.canInput = true;
                    //pauseController.canPause = true;

                    objectManager.StartMove();

                    isFirstStatePlay = true;
                }

                if (playerHPController.playerNowHP <= 0)
                {
                    gameState = GameState.GAMEOVER;
                    return;
                }

                /*if ()
                {
                    gameState = GameState.CLEAR;
                }*/

                /*if (pauseController.isPause)
                {
                    gameState = GameState.PAUSE;
                }*/

                break;
            case GameState.PAUSE:

                inputProvider.canInput = false;

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

                objectManager.StopMove();
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
