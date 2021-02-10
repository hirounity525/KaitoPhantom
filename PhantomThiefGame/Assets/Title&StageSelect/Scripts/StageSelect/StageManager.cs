using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObjs;

    public void SetActiveStages(int clearStageNum)
    {
        for (int i = 0; i < CommonData.Instance.maxStageNum; i++)
        {
            if(i == CommonData.Instance.maxStageNum)
            {
                return;
            }

            if(i < clearStageNum + 1)
            {
                stageObjs[i].SetActive(true);

                if (i == clearStageNum)
                {
                    stageObjs[i].GetComponent<SpriteRenderer>().color = Color.black;
                    stageObjs[i].GetComponent<StageCore>().isClear = false;
                }
                else
                {
                    stageObjs[i].GetComponent<SpriteRenderer>().color = Color.white;
                    stageObjs[i].GetComponent<StageCore>().isClear = true;
                }
            }
            else
            {
                stageObjs[i].SetActive(false);
            }
        }
    }

    public void SetAllActiveStages()
    {
        for (int i = 0; i < stageObjs.Length; i++)
        {
            stageObjs[i].SetActive(true);
        }
    }
}
