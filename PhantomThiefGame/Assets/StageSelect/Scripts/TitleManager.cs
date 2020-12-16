using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleState
{
    TITLE,
    CONFIG,
    EXIT,
    SAVEDATASELECT,
    NAMEINPUT,
    STAGESELECT,
    STAGEINFO
}

public class TitleManager : MonoBehaviour
{
    [SerializeField] private int clearStageNum;

    [SerializeField] private TitleState titleState;

    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private TitleEventSystemController eventSystemController;
    [SerializeField] private TimelineController timelineController;

    [Header("Title")]
    [SerializeField] private TitleMenuSelecter titleMenuSelecter;
    private bool isStateFirstPlay;

    [Header("Config")]
    [SerializeField] private SoundConfigurer soundConfigurer;

    [Header("StageSelect")]
    [SerializeField] private StageDrawer stageDrawer;
    [SerializeField] private TimelineController stageSelectedTimeline;

    [Header("StageInfo")]
    [SerializeField] private TimelineController stageStartTimeline;

    private StageManager stageManager;
    private StageSelecter stageSelecter;
    private StageDataReader stageDataReader;

    private int selectedStageNum;

    private bool startsTimeline;

    private bool startsTitleTimeline;
    private bool isStageSelectFirstPlay;
    private bool isSelectStage;
    private bool startsStageStartTimeline;

    private void Awake()
    {
        stageManager = GetComponent<StageManager>();
        stageSelecter = GetComponent<StageSelecter>();
        stageDataReader = GetComponent<StageDataReader>();
    }

    private void Start()
    {
        stageManager.SetActiveStages(clearStageNum);
    }

    // Update is called once per frame
    void Update()
    {
        switch (titleState)
        {
            case TitleState.TITLE:

                if (!isStateFirstPlay)
                {
                    titleMenuSelecter.isSelect = false;
                    isStateFirstPlay = true;
                }

                if (titleMenuSelecter.isSelect)
                {
                    switch (titleMenuSelecter.nowSelectedTitleMenu)
                    {
                        case TitleMenu.START:
                            StartStateTransition("TitleToSaveDataSelect", TitleState.SAVEDATASELECT);
                            break;
                        case TitleMenu.CONFIG:
                            StartStateTransition("TitleToConfig", TitleState.CONFIG);
                            break;
                        case TitleMenu.EXIT:
                            titleState = TitleState.EXIT;
                            break;
                    }
                }

                break;
            case TitleState.CONFIG:

                if (!isStateFirstPlay)
                {
                    soundConfigurer.nowSelectedSC = SoundConfig.MASTER;
                    soundConfigurer.canSoundConfig = true;
                    soundConfigurer.isBack = false;
                    isStateFirstPlay = true;
                }

                if (soundConfigurer.isBack)
                {
                    soundConfigurer.canSoundConfig = false;
                    StartStateTransition("ConfigToTitle", TitleState.TITLE);
                }

                break;
            case TitleState.EXIT:


                break;
            case TitleState.SAVEDATASELECT:
                break;
            case TitleState.STAGESELECT:
                if (!isSelectStage)
                {
                    if (!isStageSelectFirstPlay)
                    {
                        stageDrawer.DrawStage(clearStageNum);
                        isStageSelectFirstPlay = true;
                    }

                    if (stageDrawer.FinishDraw())
                    {
                        stageSelecter.canStageSelect = true;
                    }

                    if (titleInput.isSelectButtonDown)
                    {
                        selectedStageNum = stageSelecter.nowViewStageCore.stageNum;
                        stageDataReader.ChangeStageInfo(selectedStageNum);
                        stageSelectedTimeline.Play();
                        isSelectStage = true;
                    }
                }
                else
                {
                    if (stageSelectedTimeline.isFinish)
                    {
                        titleState = TitleState.STAGEINFO;
                    }
                }

                break;
            case TitleState.STAGEINFO:
                if (!startsStageStartTimeline)
                {
                    if (titleInput.isSelectButtonDown)
                    {
                        stageStartTimeline.Play();
                        startsStageStartTimeline = true;
                    }
                }
                else
                {
                    if (stageStartTimeline.isFinish)
                    {
                        stageDataReader.LoadStageScene(selectedStageNum);
                    }
                }


                break;
        }
    }

    private void StartStateTransition(string timelineName,TitleState transitionState)
    {
        if (!startsTimeline)
        {
            timelineController.PlayByName(timelineName);
            startsTimeline = true;
        }
        else
        {
            if (timelineController.isFinish)
            {
                titleState = transitionState;

                timelineController.ResetTimeline();
                isStateFirstPlay = false;
                startsTimeline = false;
            }
        }
    }
}
