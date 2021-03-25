using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//ステージ情報を扱う
public class StageDataReader : MonoBehaviour
{
    [SerializeField] private StageData[] stageDatas;

    [Header("StageInfo")]
    [SerializeField] private SpriteRenderer stageImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject storyTypeObj;
    [SerializeField] private GameObject missionTypeObj;
    [SerializeField] private TextMeshProUGUI outlineText;

    //ステージの情報を変更する
    public void ChangeStageInfo(int selectedStageNum, bool isClear)
    {
        StageData selectedStageData = stageDatas[selectedStageNum - 1];

        stageImage.sprite = selectedStageData.stageImage;

        if (isClear)
        {
            stageImage.color = Color.white;
        }
        else
        {
            stageImage.color = Color.black;
        }

        titleText.text = selectedStageData.stageName;

        switch (selectedStageData.stageType)
        {
            case StageType.STORY:
                storyTypeObj.SetActive(true);
                missionTypeObj.SetActive(false);
                break;
            case StageType.MISSION:
                missionTypeObj.SetActive(true);
                storyTypeObj.SetActive(false);
                break;
        }

        outlineText.text = selectedStageData.outline;

    }

    //ステージシーンへの遷移
    public void LoadStageScene(int selectedStageNum)
    {
        CommonData.Instance.selectedStageName = stageDatas[selectedStageNum - 1].sceneName;
        SceneManager.LoadScene(stageDatas[selectedStageNum - 1].sceneName);
    }
}
