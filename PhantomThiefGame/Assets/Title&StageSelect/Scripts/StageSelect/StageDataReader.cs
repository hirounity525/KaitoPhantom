using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageDataReader : MonoBehaviour
{
    [SerializeField] private StageData[] stageDatas;

    [Header("StageInfo")]
    [SerializeField] private SpriteRenderer stageImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject storyTypeObj;
    [SerializeField] private GameObject missionTypeObj;
    [SerializeField] private TextMeshProUGUI outlineText;

    public void ChangeStageInfo(int selectedStageNum)
    {
        StageData selectedStageData = stageDatas[selectedStageNum];

        stageImage.sprite = selectedStageData.stageImage;
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

    public void LoadStageScene(int selectedStageNum)
    {
        SceneManager.LoadScene(stageDatas[selectedStageNum].sceneName);
    }
}
