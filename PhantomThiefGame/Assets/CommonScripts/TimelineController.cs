using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
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

    public void Skip(float skipTime)
    {
        playableDirector.time = skipTime;
    }

    public float PlayTime()
    {
        return (float)playableDirector.time;
    }
}
