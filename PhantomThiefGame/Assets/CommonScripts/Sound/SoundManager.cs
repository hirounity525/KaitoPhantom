using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioData[] audioDatas;
    [SerializeField] private float fadeTime;

    private float maxVolume;

    private AudioSource audioSource;

    private Tweener fadeTweener;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string audioName)
    {
        AudioClip audioClip = audioDatas.FirstOrDefault(clip => clip.audioName == audioName).SEClip;

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void PlayFadeIn(string audioName)
    {
        fadeTweener.Kill();

        audioSource.Stop();
        audioSource.volume = 0;

        AudioClip audioClip = audioDatas.FirstOrDefault(clip => clip.audioName == audioName).SEClip;

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            fadeTweener = audioSource.DOFade(maxVolume, fadeTime).SetEase(Ease.InQuad);
        }
    }
}

