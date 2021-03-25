using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//各音量の状態をUIで表示
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
        //現在のボリュームで更新する
        volumeSlider.value = volumeController.nowVolume;

        //選択・非選択でカラーを変える
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
