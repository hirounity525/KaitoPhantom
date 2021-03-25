using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//ステージセレクト時、コマのアニメーション
public class StageAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 scaleUpVec;
    [SerializeField] private float animTime;

    private StageCore stageCore;
    private Transform stageTrans;

    private bool isEnlargeAnim, isShrinkAnim;

    private void Awake()
    {
        stageCore = GetComponent<StageCore>();
        stageTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //選択時
        if (stageCore.isViewed)
        {
            if (!isEnlargeAnim)
            {
                isShrinkAnim = false;

                //大きくする
                stageTrans.DOScale(scaleUpVec, animTime);

                isEnlargeAnim = true;
            }
        }
        else
        {
            if (!isShrinkAnim)
            {
                isEnlargeAnim = false;

                //小さくする
                stageTrans.DOScale(Vector3.one, animTime);

                isShrinkAnim = true;
            }
        }
    }
}
