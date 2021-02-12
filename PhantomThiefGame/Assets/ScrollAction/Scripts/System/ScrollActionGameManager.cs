using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ScrollActionGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private ScrollActionInputProvider inputProvider;
    [SerializeField] private SoundManager soundManager;

    [Header("Start")]
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string playBlockName;
    [SerializeField] private TimelineController startTimeline;

    [Header("Main")]
    [SerializeField] private ScrollActionPlayerCore playerCore;
    [SerializeField] private ScrollActionClearArea clearArea;

    [Header("Pause")]
    [SerializeField] private PauseController pauseController;

    [Header("Clear")]
    [SerializeField] private TimelineController clearTimeline;

    private SceneLoader sceneLoader;
    private CommonDataController commonDataController;

    private bool isFirstStatePlay;
    private bool isStartTimeline;

    private void Awake()
    {
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
                    pauseController.canPause = true;

                    soundManager.Play("ScrollAction");

                    isFirstStatePlay = true;
                }

                if (playerCore.isGameOver)
                {
                    gameState = GameState.GAMEOVER;
                    return;
                }

                if (clearArea.isClear)
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
