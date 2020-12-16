using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public struct TimelineData
{
    public string name;
    public TimelineAsset timelineAsset;
}

public class TimelineController : MonoBehaviour
{
    [SerializeField] private TimelineData[] timelineDatas;

    private PlayableDirector playableDirector;

    public PlayableDirector PlayableDirector
    {
        get
        {
            return playableDirector;
        }
    }

    public bool isFinish;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (!isFinish)
        {
            if (playableDirector.time >= playableDirector.duration - 0.1f)
            {
                playableDirector.Stop();
                isFinish = true;
            }
        }
    }

    public void Play()
    {
        playableDirector.Play();
    }

    public void PlayByName(string timelineName)
    {
        if(timelineDatas.Length != 0)
        {
            TimelineAsset playTimeline = timelineDatas.FirstOrDefault(timeline => timeline.name == timelineName).timelineAsset;

            if (playTimeline != null)
            {
                playableDirector.playableAsset = playTimeline;
                Play();
            }
        }
    }

    public void Skip(float skipTime)
    {
        playableDirector.time = skipTime;
    }

    public float PlayTime()
    {
        return (float)playableDirector.time;
    }

    public void ResetTimeline()
    {
        isFinish = false;
    }
}
