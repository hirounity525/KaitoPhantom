using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump3DGameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private RunJump3DInputProvider inputProvider;
    [SerializeField] private SoundManager soundManager;

    [Header("Start")]
    [SerializeField] private TimelineController startTimeline;

    [Header("Main")]
    [SerializeField] private RunJump3DPlayerCore playerCore;
    [SerializeField] private RunJump3DClearArea clearArea;

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

                    isFirstStatePlay = true;
                }

                if (!isStartTimeline)
                {
                    soundManager.PlayFadeIn("RunJump3D");
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

                break;
            case GameState.MAIN:

                if (!isFirstStatePlay)
                {
                    soundManager.Play("RunJump3D");

                    inputProvider.canInput = true;
                    pauseController.canPause = true;

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
