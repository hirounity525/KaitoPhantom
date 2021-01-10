using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundSelectAnimator : MonoBehaviour
{
    [SerializeField] private SoundConfigurer soundConfigurer;

    [Header("音量設定")]
    [SerializeField] private GameObject circleObj;
    [SerializeField] private float circleTime;
    [SerializeField] private RectTransform masterTrans;
    [SerializeField] private RectTransform bgmTrans;
    [SerializeField] private RectTransform seTrans;

    [Header("戻る")]
    [SerializeField] private RectTransform backTrans;
    [SerializeField] private Vector3 scaleUpVec;
    [SerializeField] private float scaleUpTime;
    [SerializeField] private Ease ease;
    [SerializeField] private float returnTime;

    private RectTransform circleTrans;
    private Image circleImage;

    private Tweener scaleUpTweener;

    private Vector3 firstScaleVec;
    private bool isBackAnimate;

    SoundConfig nowSelectedSC;

    // Start is called before the first frame update
    void Start()
    {
        circleTrans = circleObj.GetComponent<RectTransform>();
        circleImage = circleObj.GetComponent<Image>();

        circleImage.fillAmount = 0f;
        nowSelectedSC = soundConfigurer.nowSelectedSC;
        firstScaleVec = backTrans.localScale;

        scaleUpTweener = backTrans.DOScale(scaleUpVec, scaleUpTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        scaleUpTweener.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundConfigurer.canSoundConfig)
        {
            if(nowSelectedSC != soundConfigurer.nowSelectedSC)
            {
                if (isBackAnimate)
                {
                    scaleUpTweener.Pause();
                    backTrans.DOScale(firstScaleVec, returnTime);
                    isBackAnimate = false;
                }

                nowSelectedSC = soundConfigurer.nowSelectedSC;
                circleImage.fillAmount = 0;

                switch (nowSelectedSC)
                {
                    case SoundConfig.MASTER:
                        circleTrans.position = masterTrans.position;
                        break;
                    case SoundConfig.BGM:
                        circleTrans.position = bgmTrans.position;
                        break;
                    case SoundConfig.SE:
                        circleTrans.position = seTrans.position;
                        break;
                    case SoundConfig.BACK:
                        scaleUpTweener.Restart();
                        isBackAnimate = true;
                        break;
                }
            }

            if (!isBackAnimate)
            {
                if(circleImage.fillAmount < 1.0f)
                {
                    circleImage.fillAmount += (Time.deltaTime / circleTime);
                }
            }
        }
        else
        {
            if (isBackAnimate)
            {
                scaleUpTweener.Pause();
                backTrans.DOScale(firstScaleVec, returnTime);
                isBackAnimate = false;
            }
        }
    }
}
