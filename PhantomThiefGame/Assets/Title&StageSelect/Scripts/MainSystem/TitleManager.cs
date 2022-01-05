using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleState
{
    OPENING,
    TITLE,
    CONFIG,
    EXIT,
    SAVEDATASELECT,
    NAMEINPUT,
    CHECK,
    STAGESELECT,
    STAGEINFO
}

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TitleState titleState;

    [Header("全体")]
    [SerializeField] private TitleInputProvider titleInput;
    [SerializeField] private TitleEventSystemController eventSystemController;
    [SerializeField] private TimelineController timelineController;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SEPlayer sEPlayer;

    [Header("Opening")]
    [SerializeField] private float openingSkipTime;

    [Header("Title")]
    [SerializeField] private TitleMenuSelecter titleMenuSelecter;

    [Header("Config")]
    [SerializeField] private SoundConfigurer soundConfigurer;
    [SerializeField] private SystemDataController systemSaveDataController;

    [Header("SaveData")]
    [SerializeField] private SaveDataManager saveDataManager;
    [SerializeField] private SaveDataSelecter saveDataSelecter;

    [Header("NameInput")]
    [SerializeField] private PlayerNameSetter nameSetter;

    [Header("Check")]
    [SerializeField] private YesNoSelecter checkYesNoSelecter;

    [Header("StageSelect")]
    [SerializeField] private FadeAnimator whitePanelFade;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private StageSelecter stageSelecter;
    [SerializeField] private StageDataReader stageDataReader;
    [SerializeField] private StageDrawer stageDrawer;

    [Header("StageInfo")]
    [SerializeField] private TimelineController stageStartTimeline;

    private TitleStateBehavior stateBehavior;

    private bool isStateFirstPlay;
    private bool isTransitionFirstPlay;
    private bool isStateBack;

    private bool startsTimeline;

    private bool isEnding;

    private void Awake()
    {
        stateBehavior = GetComponent<TitleStateBehavior>();
    }

    private void Start()
    {
        //システムデータをロードする
        systemSaveDataController.Load();
        soundConfigurer.LoadSoundCofigData(systemSaveDataController.systemSaveData.soundConfigData);

        //ステージから戻ってきたとき
        if (CommonData.Instance.isBack)
        {
            //StageSelectステートへ
            titleState = TitleState.STAGESELECT;

            //ノートの状態をStageSelectの状態にする
            stateBehavior.ChangeStateBehavior(titleState);

            //ステージをクリア数だけアクティブ化する
            stageManager.SetActiveStages(CommonData.Instance.selectSaveData.clearStageNum);

            //フェードイン
            whitePanelFade.FadeIn();

            CommonData.Instance.isBack = false;
        }
    }

    void Update()
    {
        switch (titleState)
        {
            case TitleState.OPENING:

                //オープニング再生・Titleステートへ
                StartStateTransition("Opening", TitleState.TITLE);

                //スキップ
                if (titleInput.isSelectButtonDown)
                {
                    if(timelineController.PlayTime() < openingSkipTime)
                    {
                        timelineController.Skip(openingSkipTime);
                    }
                }

                break;

            case TitleState.TITLE:

                //ステートに入った最初のフレームだけ行う
                if (!isStateFirstPlay)
                {
                    //BGM再生
                    soundManager.Play("Title");

                    //変数リセット
                    titleMenuSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                //タイトルメニュー選択
                if (titleMenuSelecter.isSelect)
                {
                    switch (titleMenuSelecter.nowSelectedTitleMenu)
                    {
                        case TitleMenu.START:

                            //ステート変更中、最初のフレームだけ行う
                            if (!isTransitionFirstPlay)
                            {
                                //データロード処理
                                saveDataManager.AllLoad();
                                isTransitionFirstPlay = true;
                            }

                            //SaveDataSelectステートへ
                            StartStateTransition("TitleToSaveData", TitleState.SAVEDATASELECT);

                            break;

                        case TitleMenu.CONFIG:

                            //Configステートへ
                            StartStateTransition("TitleToConfig", TitleState.CONFIG);

                            break;

                        case TitleMenu.EXIT:

                            //Exitステートへ
                            titleState = TitleState.EXIT;

                            break;
                    }
                }

                break;

            case TitleState.CONFIG:

                //ステートに入った最初のフレームだけ行う
                if (!isStateFirstPlay)
                {
                    //変数リセット・サウンドコンフィグ可
                    soundConfigurer.nowSelectedSCM = SoundConfigMenu.MASTER;
                    soundConfigurer.canSoundConfig = true;
                    soundConfigurer.isBack = false;

                    isStateFirstPlay = true;
                }

                //戻る
                if (soundConfigurer.isBack)
                {
                    //ステート変更中、最初のフレームだけ行う
                    if (!isTransitionFirstPlay)
                    {
                        //システムデータをセーブする
                        SoundConfigData soundConfigData = soundConfigurer.NowSoundConfigData();
                        systemSaveDataController.Save(soundConfigData);

                        //サウンドコンフィグ不可
                        soundConfigurer.canSoundConfig = false;
                    }

                    //Titleステートへ
                    StartStateTransition("ConfigToTitle", TitleState.TITLE);
                }

                break;
            case TitleState.EXIT:

                //ゲーム終了
                Application.Quit();

                break;
            case TitleState.SAVEDATASELECT:

                //ステートに入った最初のフレームだけ行う
                if (!isStateFirstPlay)
                {
                    //EventSystem利用可
                    eventSystemController.EnableEventSystem();

                    //EventSystemのFirstSelectedButtonを設定
                    eventSystemController.SetSelected(titleState);

                    //変数リセット
                    saveDataSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                //ステートを戻る
                if (isStateBack)
                {
                    //Titleステートへ
                    StartStateTransition("SaveDataToTitle", TitleState.TITLE);
                    return;
                }
                else
                {
                    //キャンセルボタンが押された時
                    if (titleInput.isCancelButtonDown)
                    {
                        //SE再生
                        sEPlayer.Play("Cancel");

                        isStateBack = true;
                        return;
                    }
                }

                //セーブデータを選択した時
                if (saveDataSelecter.isSelect)
                {
                    //EventSystem利用不可
                    eventSystemController.DisableEventSystem();

                    //新しいセーブデータの時
                    if (CommonData.Instance.isSelectNewData)
                    {
                        //ステート変更中、最初のフレームだけ行う
                        if (!isTransitionFirstPlay)
                        {
                            //名前リセット
                            nameSetter.ResetName();

                            isTransitionFirstPlay = true;
                        }

                        //NameInputステートへ
                        StartStateTransition("SaveDataToNameInput", TitleState.NAMEINPUT);
                    }
                    else
                    {
                        //ステート変更中、最初のフレームだけ行う
                        if (!isTransitionFirstPlay)
                        {
                            //選択したセーブデータからクリアしたステージだけをアクティブ化する
                            stageManager.SetActiveStages(CommonData.Instance.selectSaveData.clearStageNum);

                            isTransitionFirstPlay = true;
                        }

                        //StageSelectステートへ
                        StartStateTransition("SaveDataToStageSelect", TitleState.STAGESELECT);
                    }
                }

                break;

            case TitleState.NAMEINPUT:

                //ステートに入った最初のフレームだけ行う
                if (!isStateFirstPlay)
                {
                    //EventSystem利用可
                    eventSystemController.EnableEventSystem();

                    //EventSystemのFirstSelectedButtonを設定
                    eventSystemController.SetSelected(titleState);

                    //変数リセット
                    nameSetter.isDecide = false;

                    isStateFirstPlay = true;
                }

                //ステートを戻る
                if (isStateBack)
                {
                    //SaveDataSelectステートへ
                    StartStateTransition("NameInputToSaveData", TitleState.SAVEDATASELECT);
                    return;
                }
                else
                {
                    //キャンセルボタンを押した時
                    if (titleInput.isCancelButtonDown)
                    {
                        //SE再生
                        sEPlayer.Play("Cancel");

                        //EventSystem利用不可
                        eventSystemController.DisableEventSystem();

                        isStateBack = true;
                        return;
                    }
                }

                //「決定」を押したら
                if (nameSetter.isDecide)
                {
                    //EventSystem利用不可
                    eventSystemController.DisableEventSystem();

                    //Checkステートへ
                    StartStateTransition("NameInputToCheck", TitleState.CHECK);
                }

                break;

            case TitleState.CHECK:

                //ステートに入った最初のフレームだけ行う
                if (!isStateFirstPlay)
                {
                    //EventSystem利用可
                    eventSystemController.EnableEventSystem();

                    //EventSystemのFirstSelectedButtonを設定
                    eventSystemController.SetSelected(titleState);
                    
                    //変数リセット
                    checkYesNoSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                //選択したら
                if (checkYesNoSelecter.isSelect)
                {
                    //EventSystem利用不可
                    eventSystemController.DisableEventSystem();

                    //「はい」の時
                    if (checkYesNoSelecter.isYes)
                    {
                        //ステート変更中、最初のフレームだけ行う
                        if (!isTransitionFirstPlay)
                        {
                            //裏技（100%セーブデータ）
                            if(nameSetter.playerName == "うらわざ")
                            {
                                //選択したセーブデータにステージをすべて開放して、セーブ
                                saveDataManager.AllClearSave(CommonData.Instance.selectSaveDataNum, nameSetter.playerName);

                                //ステージをクリア数だけアクティブ化する
                                stageManager.SetActiveStages(CommonData.Instance.selectSaveData.clearStageNum);

                                //新しいセーブデータにはしない
                                CommonData.Instance.isSelectNewData = false;
                            }
                            else
                            {
                                //選択したセーブデータに新しいセーブデータを作って、セーブ
                                saveDataManager.NewSave(CommonData.Instance.selectSaveDataNum, nameSetter.playerName);

                                //ステージをすべて非アクティブ化する
                                stageManager.AllDeactivateStages();
                            }

                            isTransitionFirstPlay = true;
                        }

                        //StageSelectステートへ
                        StartStateTransition("CheckToStageSelect", TitleState.STAGESELECT);
                    }

                    //「いいえ」の時
                    else
                    {
                        //NameInputステートへ
                        StartStateTransition("CheckToNameInput", TitleState.NAMEINPUT);
                    }
                }

                break;

            case TitleState.STAGESELECT:

                //ステートに入った最初のフレームだけ行う
                if (!isStateFirstPlay)
                {
                    //新しいセーブデータ、または、新しいステージをクリアして戻った時
                    if (CommonData.Instance.isSelectNewData || CommonData.Instance.isClear)
                    {
                        if (!startsTimeline)
                        {
                            //ステージを描くタイムライン再生
                            stageDrawer.DrawStage(CommonData.Instance.selectSaveData.clearStageNum);

                            startsTimeline = true;
                        }
                        else
                        {
                            //タイムライン再生後
                            if (stageDrawer.FinishDraw())
                            {
                                //共通データの変数リセット
                                CommonData.Instance.isSelectNewData = false;
                                CommonData.Instance.isClear = false;

                                //セーブ処理
                                saveDataManager.Save(CommonData.Instance.selectSaveDataNum, CommonData.Instance.selectSaveData);

                                //ステージをすべてクリアしたら
                                if(CommonData.Instance.selectSaveData.clearStageNum == CommonData.Instance.maxStageNum)
                                {
                                    //エンディングへ
                                    isEnding = true;
                                }

                                startsTimeline = false;
                            }
                        }
                        return;
                    }

                    //エンディングの時
                    if (isEnding)
                    {
                        return;
                    }

                    //BGM再生
                    soundManager.Play("Title");

                    //リセット
                    stageSelecter.SetFirstStageCore();
                    stageSelecter.canSelect = true;
                    stageSelecter.isSelect = false;

                    isStateFirstPlay = true;
                }

                //ステートを戻る
                if (isStateBack)
                {
                    //SaveDataSelectステートへ
                    StartStateTransition("StageSelectToSaveData", TitleState.SAVEDATASELECT);
                    return;
                }
                else
                {
                    //キャンセルボタンを押した時
                    if (titleInput.isCancelButtonDown)
                    {
                        //SE再生
                        sEPlayer.Play("Cancel");

                        //データロード処理
                        saveDataManager.AllLoad();

                        isStateBack = true;
                        return;
                    }
                }

                //ステージを選択した時
                if (stageSelecter.isSelect)
                {
                    //ステート変更中、最初のフレームだけ行う
                    if (!isTransitionFirstPlay)
                    {
                        //ステージ情報の変更
                        stageDataReader.ChangeStageInfo(stageSelecter.selectedStageNum, stageSelecter.selectedStageIsClear);

                        isTransitionFirstPlay = true;
                    }

                    //StageInfoステートへ
                    StartStateTransition("StageSelectToStageInfo", TitleState.STAGEINFO);
                }

                break;

            case TitleState.STAGEINFO:

                //ステートを戻る
                if (isStateBack)
                {
                    //StageSelectステートへ　
                    StartStateTransition("StageInfoToStageSelect", TitleState.STAGESELECT);
                    return;
                }

                //タイムラインを再生していない時
                if (!startsTimeline)
                {
                    //キャンセルボタンを押した時
                    if (titleInput.isCancelButtonDown)
                    {
                        //SE再生
                        sEPlayer.Play("Cancel");

                        isStateBack = true;
                        return;
                    }

                    //セレクトボタンを押した時
                    if (titleInput.isSelectButtonDown)
                    {
                        //SE再生
                        sEPlayer.Play("Select");

                        //BGMをフェードアウト
                        soundManager.StopFade();

                        //タイムライン再生
                        stageStartTimeline.Play();

                        startsTimeline = true;
                    }
                }
                else
                {
                    //タイムライン再生後
                    if (stageStartTimeline.isFinish)
                    {
                        startsTimeline = false;

                        //選択したステージシーンをロードする
                        stageDataReader.LoadStageScene(stageSelecter.selectedStageNum);
                    }
                }

                break;
        }
    }


    //タイムライン再生・ステート変更・変数リセット
    private void StartStateTransition(string timelineName,TitleState transitionState)
    {
        //タイムラインを再生しているか
        if (!startsTimeline)
        {
            //タイムライン再生
            timelineController.PlayByName(timelineName);
            startsTimeline = true;
        }
        else
        {
            //再生が終わったら
            if (timelineController.isFinish)
            {
                //ステート変更
                titleState = transitionState;

                //リセット
                timelineController.ResetTimeline();

                isStateFirstPlay = false;
                isTransitionFirstPlay = false;
                isStateBack = false;

                startsTimeline = false;
            }
        }
    }
}
