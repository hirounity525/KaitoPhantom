using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//タイトルメニューのアニメーション
public class TitleMenuAnimator : MonoBehaviour
{
    [SerializeField] private TitleMenuSelecter titleMenuSelecter;

    [SerializeField] private TitleMenu titleMenu;

    [Header("アニメーション")]
    [SerializeField] private Vector3 scaleUpVec;
    [SerializeField] private float scaleUpTime;
    [SerializeField] private Ease ease;
    [SerializeField] private float returnTime;

    private RectTransform titleMenuTrans;

    private Vector3 firstScaleVec;

    private bool isAnimate;

    private Tweener scaleUpTweener;

    private void Awake()
    {
        titleMenuTrans = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //最初のサイズを記録する
        firstScaleVec = titleMenuTrans.localScale;

        //DoTweenをセットする
        scaleUpTweener = titleMenuTrans.DOScale(scaleUpVec, scaleUpTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        scaleUpTweener.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        //決定していない時
        if (!titleMenuSelecter.isSelect)
        {
            //選択しているのが自身と一致していたら
            if (titleMenu == titleMenuSelecter.nowSelectedTitleMenu)
            {
                if (!isAnimate)
                {
                    //アニメーション開始
                    scaleUpTweener.Restart();
                    isAnimate = true;
                }
            }
            else
            {
                if (isAnimate)
                {
                    //アニメーションストップ・元の大きさに戻す
                    scaleUpTweener.Pause();
                    titleMenuTrans.DOScale(firstScaleVec, returnTime);
                    isAnimate = false;
                }
            }
        }
    }
}
