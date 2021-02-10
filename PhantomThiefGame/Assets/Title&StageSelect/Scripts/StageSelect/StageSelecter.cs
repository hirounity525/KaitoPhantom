using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelecter : MonoBehaviour
{
    public StageCore nowViewStageCore;
    public int selectedStageNum;
    public bool selectedStageIsClear;
    public bool canSelect;
    public bool isSelect;

    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private SEPlayer sEPlayer;

    [SerializeField] private StageCore firstViewStageCore;

    // Start is called before the first frame update
    void Start()
    {
        SetFirstStageCore();
    }

    private void Update()
    {
        if (canSelect && !isSelect)
        {
            SelectStage();
        }
    }

    private void SelectStage()
    {
        if (!nowViewStageCore.isViewed)
        {
            nowViewStageCore.isViewed = true;
        }

        if (titleInput.isMoveButtonDown)
        {
            sEPlayer.Play("MenuMove");

            StageCore nextStageCore = null;

            switch (titleInput.moveArrow)
            {
                case InputArrow.UP:
                    nextStageCore = nowViewStageCore.nextStages.stageNumU;
                    break;
                case InputArrow.DOWN:
                    nextStageCore = nowViewStageCore.nextStages.stageNumD;
                    break;
                case InputArrow.RIGHT:
                    nextStageCore = nowViewStageCore.nextStages.stageNumR;
                    break;
                case InputArrow.LEFT:
                    nextStageCore = nowViewStageCore.nextStages.stageNumL;
                    break;
            }

            if (nextStageCore != null && nextStageCore.gameObject.activeSelf)
            {
                nowViewStageCore.isViewed = false;
                nowViewStageCore = nextStageCore;
            }
        }

        if (titleInput.isSelectButtonDown)
        {
            sEPlayer.Play("Select");

            nowViewStageCore.isViewed = false;
            canSelect = false;

            selectedStageNum = nowViewStageCore.stageNum;
            selectedStageIsClear = nowViewStageCore.isClear;
            CommonData.Instance.selectedStageNum = selectedStageNum;

            isSelect = true;
        }
    }

    public void SetFirstStageCore()
    {
        nowViewStageCore = firstViewStageCore;
    }
}
