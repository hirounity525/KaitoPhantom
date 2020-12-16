using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        firstScaleVec = titleMenuTrans.localScale;

        scaleUpTweener = titleMenuTrans.DOScale(scaleUpVec, scaleUpTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        scaleUpTweener.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!titleMenuSelecter.isSelect)
        {
            if (titleMenu == titleMenuSelecter.nowSelectedTitleMenu)
            {
                if (!isAnimate)
                {
                    scaleUpTweener.Restart();
                    isAnimate = true;
                }
            }
            else
            {
                if (isAnimate)
                {
                    scaleUpTweener.Pause();
                    titleMenuTrans.DOScale(firstScaleVec, returnTime);
                    isAnimate = false;
                }
            }
        }
    }
}
