using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//上下左右のStageCore情報
[System.Serializable]
public struct NextStageNum
{
    public StageCore stageNumU;
    public StageCore stageNumD;
    public StageCore stageNumR;
    public StageCore stageNumL;
}

//ステージセレクトコマの情報
public class StageCore : MonoBehaviour
{
    public int stageNum;
    public NextStageNum nextStages;
    public bool isViewed;
    public bool isClear;
}
