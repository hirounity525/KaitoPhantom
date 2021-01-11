using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelecter : MonoBehaviour
{
    public StageCore nowViewStageCore;
    public bool canSelect;
    public bool isSelect;

    [SerializeField] private TitleInputProvider titleInput;

    [SerializeField] private StageCore firstViewStageCore;

    // Start is called before the first frame update
    void Start()
    {
        nowViewStageCore = firstViewStageCore;
    }

    private void Update()
    {
        if (canSelect && !isSelect)
        {
            SelectStage();
        }
    }

    public void SelectStage()
    {
        if (!nowViewStageCore.isViewed)
        {
            nowViewStageCore.isViewed = true;
        }

        if (titleInput.isMoveButtonDown)
        {
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
            nowViewStageCore.isViewed = false;
            canSelect = false;
            isSelect = true;
        }
    }
}
