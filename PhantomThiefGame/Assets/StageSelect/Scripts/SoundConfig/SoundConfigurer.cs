using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundConfig
{
    MASTER,
    BGM,
    SE,
    BACK
}

public class SoundConfigurer : MonoBehaviour
{
    public SoundConfig nowSelectedSC;

    public bool isBack;

    public bool canSoundConfig;

    [SerializeField] private TitleInputProvider titleInput;

    [SerializeField] private SoundConfig firstSelectedSC;

    [SerializeField] private SoundVolumeController masterVC;
    [SerializeField] private SoundVolumeController bgmVC;
    [SerializeField] private SoundVolumeController seVC;

    private SoundVolumeController selectedVC;

    private void Start()
    {
        nowSelectedSC = firstSelectedSC;
    }

    private void Update()
    {
        if (!canSoundConfig)
        {
            return;
        }

        if(selectedVC == null)
        {
            if (titleInput.isMoveButtonDown)
            {
                switch (titleInput.moveArrow)
                {
                    case InputArrow.UP:
                        if (nowSelectedSC != SoundConfig.MASTER)
                        {
                            nowSelectedSC--;
                        }
                        break;
                    case InputArrow.DOWN:
                        if (nowSelectedSC != SoundConfig.BACK)
                        {
                            nowSelectedSC++;
                        }
                        break;
                }
            }

            if (titleInput.isSelectButtonDown)
            {
                switch (nowSelectedSC)
                {
                    case SoundConfig.MASTER:
                        selectedVC = masterVC;
                        selectedVC.canConfig = true;
                        break;
                    case SoundConfig.BGM:
                        selectedVC = bgmVC;
                        selectedVC.canConfig = true;
                        break;
                    case SoundConfig.SE:
                        selectedVC = seVC;
                        selectedVC.canConfig = true;
                        break;
                    case SoundConfig.BACK:
                        isBack = true;
                        break;
                }
            }
        }
        else
        {
            if (selectedVC.isFinish)
            {
                selectedVC.isFinish = false;
                selectedVC = null;
            }
        }
    }
}
