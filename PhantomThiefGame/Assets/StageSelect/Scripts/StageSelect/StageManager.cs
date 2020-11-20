using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObjs;

    public void SetActiveStages(int clearStageNum)
    {
        for(int i = 0; i < clearStageNum; i++)
        {
            stageObjs[i].SetActive(true);
        }
    }
}
