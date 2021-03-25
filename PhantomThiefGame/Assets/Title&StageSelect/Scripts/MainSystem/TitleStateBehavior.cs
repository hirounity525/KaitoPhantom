using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステートに合わせて、状態を変える処理
public class TitleStateBehavior : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject titleVC;
    [SerializeField] private GameObject stageSelectVC;

    [Header("Title")]
    [SerializeField] private GameObject titleCanvas;

    [Header("StageSelect")]
    [SerializeField] private GameObject stageObjLeft;
    [SerializeField] private GameObject stageObjRight;

    [Header("Note")]
    [SerializeField] private Transform[] notePageTrans;
    [SerializeField] private Transform[] pageOpenTrans;

    //ステートに合わせて、カメラ・ノートの状態を変える
    public void ChangeStateBehavior(TitleState nowState)
    {
        if(nowState == TitleState.STAGESELECT)
        {
            titleVC.SetActive(false);
            stageSelectVC.SetActive(true);

            stageObjRight.SetActive(true);
            stageObjLeft.SetActive(true);
            titleCanvas.SetActive(false);

           for(int i = 0; i < 4; i++)
            {
                notePageTrans[i].localRotation = pageOpenTrans[i].rotation;
            }
        }
    }
}
