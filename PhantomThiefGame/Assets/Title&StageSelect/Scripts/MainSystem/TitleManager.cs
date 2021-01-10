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
    CHECK,
    STAGESELECT,
    STAGEINFO
}

public class TitleManager : MonoBehaviour
{
    [SerializeField] private int clearStageNum;

    [SerializeField] private TitleState titleState;

    [Header("全体")]
    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private TitleEventSystemController eventSystemController;
    [SerializeField] private TimelineController timelineController;

    [Header("Title")]
    [SerializeField] private TitleMenuSelecter titleMenuSelecter;

    [Header("Config")]
    [SerializeField] private SoundConfigurer soundConfigurer;

    [Header("SaveData")]
    [SerializeField] private SaveDataManager saveDataManager;
    [SerializeField] private SaveDataSelecter saveDataSelecter;

    [Header("NameInput")]
    [SerializeField] private PlayerNameSetter nameSetter;

    [Header("Check")]
    [SerializeField] private YesNoSelecter checkYesNoSelecter;

    [Header("StageSelect")]
    [SerializeField] private StageManager stageManager;
    [SerializeField] private StageSelecter stageSelecter;
    [SerializeField] private StageDataReader stageDataReader;
    [SerializeField] private StageDrawer stageDrawer;
    [SerializeField] private TimelineController stageSelectedTimeline;

    [Header("StageInfo")]
    [SerializeField] private TimelineController stageStartTimeline;

    private bool isStateFirstPlay;
    private bool isTransitionFirstPlay;
    private bool isStateBack;

    private int selectedStageNum;

    private bool startsTimeline;

    private bool startsTitleTimeline;
    private bool isStageSelectFirstPlay;
    private bool isSelectStage;
    private bool startsStageStartTimeline;

    private void Awake()
    {

    }

    private void Start()
    {
        if (DebugModeInfo.Instance.isBackTitle)
        {
            titleState = TitleState.SAVEDATASELECT;
            stageManager.SetAllActiveStages();
            return;
        }

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

                            if (DebugModeInfo.Instance.isTtoSSMode)
                            {
                                StartStateTransition("TitleToStageSelect", TitleState.STAGESELECT);
                                return;
                            }

                            if (!isTransitionFirstPlay)
                            {
                                saveDataManager.AllLoad();
                                isTransitionFirstPlay = true;
                            }

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

                Application.Quit();

                break;
            case TitleState.SAVEDATASELECT:

                if (!isStateFirstPlay)
                {
                    eventSystemController.EnableEventSystem();
                    eventSystemController.SetSelected(titleState);
                    saveDataSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                if (isStateBack)
                {
                    StartStateTransition("SaveDataSelectToTitle", TitleState.TITLE);
                    return;
                }

                if (titleInput.isCancelButtonDown)
                {
                    isStateBack = true;
                }

                if (saveDataSelecter.isSelect)
                {
                    eventSystemController.DisableEventSystem();

                    if (saveDataSelecter.isSelectNewData)
                    {
                        if (!isTransitionFirstPlay)
                        {
                            nameSetter.ResetName();
                            isTransitionFirstPlay = true;
                        }

                        StartStateTransition("SaveDataSelectToNameInput", TitleState.NAMEINPUT);
                    }
                    else
                    {

                    }
                }

                break;
            case TitleState.NAMEINPUT:

                if (!isStateFirstPlay)
                {
                    eventSystemController.EnableEventSystem();
                    eventSystemController.SetSelected(titleState);
                    nameSetter.isDecide = false;

                    isStateFirstPlay = true;
                }

                if (isStateBack)
                {
                    StartStateTransition("NameInputToSaveDataSelect", TitleState.SAVEDATASELECT);
                    return;
                }

                if (titleInput.isCancelButtonDown)
                {
                    eventSystemController.DisableEventSystem();
                    isStateBack = true;
                }

                if (nameSetter.isDecide)
                {
                    eventSystemController.DisableEventSystem();

                    StartStateTransition("NameInputToCheck", TitleState.CHECK);
                }

                break;
            case TitleState.CHECK:

                if (!isStateFirstPlay)
                {
                    eventSystemController.EnableEventSystem();
                    eventSystemController.SetSelected(titleState);
                    checkYesNoSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                if (checkYesNoSelecter.isSelect)
                {
                    eventSystemController.DisableEventSystem();

                    if (checkYesNoSelecter.isYes)
                    {
                        if (!isTransitionFirstPlay)
                        {
                            saveDataManager.NewSave(saveDataSelecter.selectSaveDataNum, nameSetter.playerName);
                            isTransitionFirstPlay = true;
                        }


                    }
                    else
                    {
                        StartStateTransition("CheckToNameInput", TitleState.NAMEINPUT);
                    }
                }

                break;
            case TitleState.STAGESELECT:
                if (!isStageSelectFirstPlay)
                {
                    stageSelecter.canStageSelect = true;
                    isStageSelectFirstPlay = true;
                }

                if (titleInput.isSelectButtonDown)
                {
                    selectedStageNum = stageSelecter.nowViewStageCore.stageNum;
                    stageDataReader.ChangeStageInfo(selectedStageNum);
                    stageSelectedTimeline.Play();
                    isSelectStage = true;
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
                isTransitionFirstPlay = false;
                isStateBack = false;

                startsTimeline = false;
            }
        }
    }
}
