using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDataController : MonoBehaviour
{
    public void ClearStage()
    {
        CommonData.Instance.isBack = true;

        if (CommonData.Instance.selectedStageNum > CommonData.Instance.selectSaveData.clearStageNum)
        {
            CommonData.Instance.selectSaveData.clearStageNum = CommonData.Instance.selectedStageNum;
            CommonData.Instance.isClear = true;
        }
    }

    public void BackTitle()
    {
        CommonData.Instance.isBack = true;
    }
}
