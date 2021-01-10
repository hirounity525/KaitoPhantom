using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIDrawer : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Image handle;
    [SerializeField] private Color selectColor;
    [SerializeField] private Color submitColor;

    SoundVolumeController volumeController;

    private void Awake()
    {
        volumeController = GetComponent<SoundVolumeController>();
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.value = volumeController.nowVolume;

        if (volumeController.canConfig)
        {
            handle.color = selectColor;
        }
        else
        {
            handle.color = submitColor;
        }
    }
}
