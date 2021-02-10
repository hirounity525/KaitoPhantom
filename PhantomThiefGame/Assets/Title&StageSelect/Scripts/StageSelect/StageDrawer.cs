using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class StageDrawer : MonoBehaviour
{
    [SerializeField] private TimelineAsset[] stageDrawTimelines;

    TimelineController stageTimeline;

    private void Awake()
    {
        stageTimeline = GetComponent<TimelineController>();
    }

    public void DrawStage(int clearStageNum)
    {
        if (stageDrawTimelines[clearStageNum] != null)
        {
            stageTimeline.PlayableDirector.playableAsset = stageDrawTimelines[clearStageNum];
            stageTimeline.Play();
        }
    }

    public bool FinishDraw()
    {
        return stageTimeline.isFinish;
    }
}
