using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    STORY,
    MISSION
}

[CreateAssetMenu(menuName = "Stage/StageData")]
public class StageData : ScriptableObject
{
    public int stageNum;
    public Sprite stageImage;
    public string stageName;
    public StageType stageType;
    [TextArea(2,5)]public string outline;
    public string sceneName;
}
