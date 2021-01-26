using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleState
{
    TITLE,
    SAVEDATASELECT,
    STAGESELECT,
    STAGEINFO
}

public class TitleManager : MonoBehaviour
{
    [SerializeField] private int clearStageNum;

    [SerializeField] private TitleState titleState;

    [SerializeField] private TitleInputProvider titleInput;

    [Header("Title")]
    [SerializeField] private TimelineController titleTimeline;

    [Header("StageSelect")]
    [SerializeField] private StageDrawer stageDrawer;
    [SerializeField] private TimelineController stageSelectedTimeline;

    [Header("StageInfo")]
    [SerializeField] private TimelineController stageStartTimeline;

    private StageManager stageManager;
    private StageSelecter stageSelecter;
    private StageDataReader stageDataReader;

    private int selectedStageNum;

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
                if (!startsTitleTimeline)
                {
                    if (titleInput.isSelectButtonDown)
                    {
                        titleTimeline.Play();
                        startsTitleTimeline = true;
                    }
                }
                else
                {
                    if (titleTimeline.isFinish)
                    {
                        titleState = TitleState.STAGESELECT;
                    }
                }

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
}
