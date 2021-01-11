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

    [Header("StageInfo")]
    [SerializeField] private TimelineController stageStartTimeline;

    private bool isStateFirstPlay;
    private bool isTransitionFirstPlay;
    private bool isStateBack;

    private int selectedStageNum;

    private bool startsTimeline;

    private bool startsStageStartTimeline;

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

                        StartStateTransition("SaveDataToNameInput", TitleState.NAMEINPUT);
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
                    StartStateTransition("NameInputToSaveData", TitleState.SAVEDATASELECT);
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

                if (!isStateFirstPlay)
                {
                    stageSelecter.canSelect = true;
                    stageSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                //修正対象
                if (isStateBack)
                {
                    StartStateTransition("StageSelectToTitle", TitleState.TITLE);
                    return;
                }

                if (!startsTimeline)
                {
                    if (titleInput.isCancelButtonDown)
                    {
                        isStateBack = true;
                        return;
                    }
                }
                //

                if (stageSelecter.isSelect)
                {
                    selectedStageNum = stageSelecter.nowViewStageCore.stageNum;
                    //stageDataReader.ChangeStageInfo(selectedStageNum);
                    //トランジション用のenum作るのあり
                    StartStateTransition("StageSelectToStageInfo", TitleState.STAGEINFO);
                }

                break;
            case TitleState.STAGEINFO:

                //修正対象

                if (isStateBack)
                {
                    StartStateTransition("StageInfoToStageSelect", TitleState.STAGESELECT);
                    return;
                }

                if (!startsStageStartTimeline)
                {
                    if (titleInput.isCancelButtonDown)
                    {
                        isStateBack = true;
                        return;
                    }

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
                //


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
