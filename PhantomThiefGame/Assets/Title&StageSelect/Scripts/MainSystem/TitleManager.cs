using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleState
{
    OPENING,
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
    [SerializeField] private TitleState titleState;

    [Header("全体")]
    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private TitleEventSystemController eventSystemController;
    [SerializeField] private TimelineController timelineController;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SEPlayer sEPlayer;

    [Header("Opening")]
    [SerializeField] private float openingSkipTime;

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
    [SerializeField] private FadeAnimator whitePanelFade;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private StageSelecter stageSelecter;
    [SerializeField] private StageDataReader stageDataReader;
    [SerializeField] private StageDrawer stageDrawer;

    [Header("StageInfo")]
    [SerializeField] private TimelineController stageStartTimeline;

    private TitleStateBehavior stateBehavior;

    private bool isStateFirstPlay;
    private bool isTransitionFirstPlay;
    private bool isStateBack;

    private bool startsTimeline;

    private bool isEnding;

    private void Awake()
    {
        stateBehavior = GetComponent<TitleStateBehavior>();
    }

    private void Start()
    {
        if (CommonData.Instance.isBack)
        {
            titleState = TitleState.STAGESELECT;

            stateBehavior.ChangeStateBehavior(titleState);
            stageManager.SetActiveStages(CommonData.Instance.selectSaveData.clearStageNum);

            whitePanelFade.FadeIn();

            CommonData.Instance.isBack = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (titleState)
        {
            case TitleState.OPENING:

                StartStateTransition("Opening", TitleState.TITLE);

                if (titleInput.isSelectButtonDown)
                {
                    if(timelineController.PlayTime() < openingSkipTime)
                    {
                        timelineController.Skip(openingSkipTime);
                    }
                }

                break;

            case TitleState.TITLE:

                if (!isStateFirstPlay)
                {
                    soundManager.Play("Title");
                    titleMenuSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                if (titleMenuSelecter.isSelect)
                {
                    switch (titleMenuSelecter.nowSelectedTitleMenu)
                    {
                        case TitleMenu.START:

                            if (!isTransitionFirstPlay)
                            {
                                saveDataManager.AllLoad();
                                isTransitionFirstPlay = true;
                            }

                            StartStateTransition("TitleToSaveData", TitleState.SAVEDATASELECT);

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
                    StartStateTransition("SaveDataToTitle", TitleState.TITLE);
                    return;
                }

                if (!startsTimeline && titleInput.isCancelButtonDown)
                {
                    sEPlayer.Play("Cancel");
                    isStateBack = true;
                    return;
                }

                if (saveDataSelecter.isSelect)
                {
                    eventSystemController.DisableEventSystem();

                    if (CommonData.Instance.isSelectNewData)
                    {
                        if (!isTransitionFirstPlay)
                        {
                            nameSetter.ResetName();
                            isTransitionFirstPlay = true;
                        }

                        StartStateTransition("SaveDataToNameInput", TitleState.NAMEINPUT);
                    }
                    else
                    {
                        if (!isTransitionFirstPlay)
                        {
                            stageManager.SetActiveStages(CommonData.Instance.selectSaveData.clearStageNum);
                            isTransitionFirstPlay = true;
                        }

                        StartStateTransition("SaveDataToStageSelect", TitleState.STAGESELECT);
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
                    StartStateTransition("NameInputToSaveData", TitleState.SAVEDATASELECT);
                    return;
                }

                if (!startsTimeline && titleInput.isCancelButtonDown)
                {
                    sEPlayer.Play("Cancel");
                    eventSystemController.DisableEventSystem();
                    isStateBack = true;
                    return;
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
                            saveDataManager.NewSave(CommonData.Instance.selectSaveDataNum, nameSetter.playerName);
                            isTransitionFirstPlay = true;
                        }

                        eventSystemController.DisableEventSystem();

                        StartStateTransition("CheckToStageSelect", TitleState.STAGESELECT);
                    }
                    else
                    {
                        StartStateTransition("CheckToNameInput", TitleState.NAMEINPUT);
                    }
                }

                break;
            case TitleState.STAGESELECT:

                if (!isStateFirstPlay)
                {
                    if (CommonData.Instance.isSelectNewData || CommonData.Instance.isClear)
                    {
                        if (!startsTimeline)
                        {
                            stageDrawer.DrawStage(CommonData.Instance.selectSaveData.clearStageNum);
                            startsTimeline = true;
                        }
                        else
                        {
                            if (stageDrawer.FinishDraw())
                            {
                                CommonData.Instance.isSelectNewData = false;
                                CommonData.Instance.isClear = false;

                                saveDataManager.Save(CommonData.Instance.selectSaveDataNum, CommonData.Instance.selectSaveData);

                                startsTimeline = false;

                                if(CommonData.Instance.selectSaveData.clearStageNum == CommonData.Instance.maxStageNum)
                                {
                                    isEnding = true;
                                }
                            }
                        }
                        return;
                    }

                    if (isEnding)
                    {
                        return;
                    }

                    soundManager.Play("Title");

                    stageSelecter.SetFirstStageCore();
                    stageSelecter.canSelect = true;
                    stageSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                if (isStateBack)
                {
                    StartStateTransition("StageSelectToSaveData", TitleState.SAVEDATASELECT);
                    return;
                }

                if (!startsTimeline)
                {
                    if (titleInput.isCancelButtonDown)
                    {
                        sEPlayer.Play("Cancel");
                        saveDataManager.AllLoad();
                        isStateBack = true;
                        return;
                    }
                }

                if (stageSelecter.isSelect)
                {
                    stageDataReader.ChangeStageInfo(stageSelecter.selectedStageNum, stageSelecter.selectedStageIsClear);

                    StartStateTransition("StageSelectToStageInfo", TitleState.STAGEINFO);
                }

                break;
            case TitleState.STAGEINFO:

                if (isStateBack)
                {
                    StartStateTransition("StageInfoToStageSelect", TitleState.STAGESELECT);
                    return;
                }

                if (!startsTimeline)
                {
                    if (titleInput.isCancelButtonDown)
                    {
                        sEPlayer.Play("Cancel");
                        isStateBack = true;
                        return;
                    }

                    if (titleInput.isSelectButtonDown)
                    {
                        sEPlayer.Play("Select");
                        soundManager.StopFade();
                        stageStartTimeline.Play();
                        startsTimeline = true;
                    }
                }
                else
                {
                    if (stageStartTimeline.isFinish)
                    {
                        startsTimeline = false;
                        stageDataReader.LoadStageScene(stageSelecter.selectedStageNum);
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
