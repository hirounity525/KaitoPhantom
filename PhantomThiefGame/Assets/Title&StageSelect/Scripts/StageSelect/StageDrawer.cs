using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

//ステージが増えるタイムラインの管理・タイムラインコントローラーの拡張
public class StageDrawer : MonoBehaviour
{
    [SerializeField] private TimelineAsset[] stageDrawTimelines;

    TimelineController stageTimeline;

    private void Awake()
    {
        stageTimeline = GetComponent<TimelineController>();
    }

    //対応したタイムラインの再生
    public void DrawStage(int clearStageNum)
    {
        if (stageDrawTimelines[clearStageNum] != null)
        {
            stageTimeline.PlayableDirector.playableAsset = stageDrawTimelines[clearStageNum];
            stageTimeline.Play();
        }
    }

    //タイムラインの再生が終了しているか
    public bool FinishDraw()
    {
        return stageTimeline.isFinish;
    }
}
