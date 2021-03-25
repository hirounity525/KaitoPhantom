using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音量設定メニュー
public enum SoundConfigMenu
{
    MASTER,
    BGM,
    SE,
    BACK
}

//音量設定全体の処理
public class SoundConfigurer : MonoBehaviour
{
    public SoundConfigMenu nowSelectedSCM;

    public bool isBack;

    public bool canSoundConfig;

    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private SEPlayer sEPlayer;

    [SerializeField] private SoundConfigMenu firstSelectedSC;

    [SerializeField] private SoundVolumeController masterVC;
    [SerializeField] private SoundVolumeController bgmVC;
    [SerializeField] private SoundVolumeController seVC;

    private SoundVolumeController selectedVC;

    private SoundConfigData nowSoundConfigData;

    private void Start()
    {
        nowSelectedSCM = firstSelectedSC;
    }

    private void Update()
    {
        //設定をできなくする
        if (!canSoundConfig)
        {
            return;
        }

        //選択をしていない時
        if(selectedVC == null)
        {
            //移動ボタン入力時
            if (titleInput.isMoveButtonDown)
            {
                //選択しているメニューの切り替え
                switch (titleInput.moveArrow)
                {
                    case InputArrow.UP:
                        if (nowSelectedSCM != SoundConfigMenu.MASTER)
                        {
                            sEPlayer.Play("ChokeCircle");
                            nowSelectedSCM--;
                        }
                        break;
                    case InputArrow.DOWN:
                        if (nowSelectedSCM != SoundConfigMenu.BACK)
                        {
                            sEPlayer.Play("ChokeCircle");
                            nowSelectedSCM++;
                        }
                        break;
                }
            }

            //選択ボタン入力時
            if (titleInput.isSelectButtonDown)
            {
                switch (nowSelectedSCM)
                {
                    //マスターボリュームを設定できるようにする
                    case SoundConfigMenu.MASTER:
                        selectedVC = masterVC;
                        selectedVC.canConfig = true;
                        sEPlayer.Play("ChokeSelect");
                        break;
                    //BGMボリュームを設定できるようにする
                    case SoundConfigMenu.BGM:
                        selectedVC = bgmVC;
                        selectedVC.canConfig = true;
                        sEPlayer.Play("ChokeSelect");
                        break;
                    //SEボリュームを設定できるようにする
                    case SoundConfigMenu.SE:
                        selectedVC = seVC;
                        selectedVC.canConfig = true;
                        sEPlayer.Play("ChokeSelect");
                        break;
                    //戻る
                    case SoundConfigMenu.BACK:
                        sEPlayer.Play("Select");
                        isBack = true;
                        break;
                }
            }
        }
        else
        {
            //選択した設定が終了したら、選択できるようにする
            if (selectedVC.isFinish)
            {
                selectedVC.isFinish = false;
                selectedVC = null;
            }
        }
    }

    //現在の各ボリューム
    public SoundConfigData NowSoundConfigData()
    {
        nowSoundConfigData.masterVolume = masterVC.nowVolume;
        nowSoundConfigData.bgmVolume = bgmVC.nowVolume;
        nowSoundConfigData.seVolume = seVC.nowVolume;

        return nowSoundConfigData;
    }

    //各ボリュームのデータロード
    public void LoadSoundCofigData(SoundConfigData soundConfigData)
    {
        masterVC.nowVolume = soundConfigData.masterVolume;
        bgmVC.nowVolume = soundConfigData.bgmVolume;
        seVC.nowVolume = soundConfigData.seVolume;

        masterVC.SetMixerVolume();
        bgmVC.SetMixerVolume();
        seVC.SetMixerVolume();
    }
}
