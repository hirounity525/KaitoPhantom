using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStateBehavior : MonoBehaviour
{
    [SerializeField] private GameObject titleVC;
    [SerializeField] private GameObject stageSelectVC;

    [SerializeField] private GameObject stageObjLeft;
    [SerializeField] private GameObject stageObjRight;
    [SerializeField] private GameObject titleCanvas;

    [SerializeField] private Transform[] notePageTrans;

    [SerializeField] private Transform[] pageOpenTrans;

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
