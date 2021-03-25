using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//音量設定全体のアニメーション処理
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

    SoundConfigMenu nowSelectedSCM;

    // Start is called before the first frame update
    void Start()
    {
        circleTrans = circleObj.GetComponent<RectTransform>();
        circleImage = circleObj.GetComponent<Image>();

        //リセット
        circleImage.fillAmount = 0f;
        nowSelectedSCM = soundConfigurer.nowSelectedSCM;
        firstScaleVec = backTrans.localScale;

        //「戻る」のDoTweenアニメーションを設定し、止めておく
        scaleUpTweener = backTrans.DOScale(scaleUpVec, scaleUpTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        scaleUpTweener.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        //音量設定ができる時
        if (soundConfigurer.canSoundConfig)
        {
            //選択しているメニューが変わった時
            if(nowSelectedSCM != soundConfigurer.nowSelectedSCM)
            {
                //戻るアニメーションを再生してたら、アニメーションを止め、元の大きさにリセット
                if (isBackAnimate)
                {
                    scaleUpTweener.Pause();
                    backTrans.DOScale(firstScaleVec, returnTime);
                    isBackAnimate = false;
                }

                //選択してるメニューの更新
                nowSelectedSCM = soundConfigurer.nowSelectedSCM;
                
                //リセット
                circleImage.fillAmount = 0;

                switch (nowSelectedSCM)
                {
                    //サークルUIをMASTERの位置に移動させる
                    case SoundConfigMenu.MASTER:
                        circleTrans.position = masterTrans.position;
                        break;
                    //サークルUIをBGMの位置に移動させる
                    case SoundConfigMenu.BGM:
                        circleTrans.position = bgmTrans.position;
                        break;
                    //サークルUIをSEの位置に移動させる
                    case SoundConfigMenu.SE:
                        circleTrans.position = seTrans.position;
                        break;
                    //戻るアニメーションを再生させる
                    case SoundConfigMenu.BACK:
                        scaleUpTweener.Restart();
                        isBackAnimate = true;
                        break;
                }
            }

            //戻るを選択していなかったら、サークルUIをアニメーションさせる
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
            //リセット
            if (isBackAnimate)
            {
                scaleUpTweener.Pause();
                backTrans.DOScale(firstScaleVec, returnTime);
                isBackAnimate = false;
            }
        }
    }
}
