using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundVolumeController : MonoBehaviour
{
    [Range(0, 1)] public float nowVolume;

    public bool canConfig;
    public bool isFinish;

    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private SEPlayer sEPlayer;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeName;

    [SerializeField] private float volumeChangeAmount;

    private bool firstSelected = true;

    // Update is called once per frame
    void Update()
    {
        //設定できるようになっていたら
        if (canConfig)
        {
            //1フレーム遅らせる
            if (firstSelected)
            {
                firstSelected = false;
                return;
            }

            //移動ボタン入力時
            if (titleInput.isMoveButtonDown)
            {
                sEPlayer.Play("ChokeSelect");

                switch (titleInput.moveArrow)
                {
                    //音量を上げる
                    case InputArrow.RIGHT:
                        nowVolume += volumeChangeAmount;
                        nowVolume = Mathf.Clamp01(nowVolume);
                        SetMixerVolume();
                        break;
                    //音量を下げる
                    case InputArrow.LEFT:
                        nowVolume -= volumeChangeAmount;
                        nowVolume = Mathf.Clamp01(nowVolume);
                        SetMixerVolume();
                        break;
                }
            }

            //選択・戻るボタン入力時
            if(titleInput.isSelectButtonDown || titleInput.isCancelButtonDown)
            {
                sEPlayer.Play("ChokeSelect");

                //リセットし、設定を終了する
                canConfig = false;
                firstSelected = true;
                isFinish = true;
            }
        }
    }

    //AudioMixerに現在のボリュームを反映させる
    public void SetMixerVolume()
    {
        float decibel = Mathf.Clamp(20f * Mathf.Log10(nowVolume), -80f, 0f);
        audioMixer.SetFloat(volumeName, decibel);
    }
}
