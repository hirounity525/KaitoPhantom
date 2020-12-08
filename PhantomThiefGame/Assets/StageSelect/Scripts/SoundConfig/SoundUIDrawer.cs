using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIDrawer : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    SoundVolumeController volumeController;

    private void Awake()
    {
        volumeController = GetComponent<SoundVolumeController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.value = volumeController.nowVolume;
    }
}
