using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NextStageNum
{
    public StageCore stageNumU;
    public StageCore stageNumD;
    public StageCore stageNumR;
    public StageCore stageNumL;
}

public class StageCore : MonoBehaviour
{
    public int stageNum;
    public NextStageNum nextStages;
    public bool isViewed;
    public bool isClear;
}
