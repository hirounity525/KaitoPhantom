using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundVolumeController : MonoBehaviour
{
    [Range(0, 1)] public float nowVolume;

    public bool canConfig;
    public bool isFinish;

    [SerializeField] private TitleInputProvider titleInput;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeName;

    [SerializeField] private float volumeChangeAmount;

    private bool firstSelected = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canConfig)
        {
            if (firstSelected)
            {
                firstSelected = false;
                return;
            }

            if (titleInput.isMoveButtonDown)
            {
                switch (titleInput.moveArrow)
                {
                    case InputArrow.RIGHT:
                        nowVolume += volumeChangeAmount;
                        nowVolume = Mathf.Clamp01(nowVolume);
                        SetMixerVolume(nowVolume);
                        break;
                    case InputArrow.LEFT:
                        nowVolume -= volumeChangeAmount;
                        nowVolume = Mathf.Clamp01(nowVolume);
                        SetMixerVolume(nowVolume);
                        break;
                }
            }

            if(titleInput.isSelectButtonDown || titleInput.isCancelButtonDown)
            {
                canConfig = false;
                firstSelected = true;
                isFinish = true;
            }
        }
    }

    private void SetMixerVolume(float volume)
    {
        float decibel = Mathf.Clamp(20f * Mathf.Log10(volume), -80f, 0f);
        audioMixer.SetFloat(volumeName, decibel);
    }
}
