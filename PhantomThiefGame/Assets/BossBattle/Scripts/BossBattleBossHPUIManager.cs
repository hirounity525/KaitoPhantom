using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattleBossHPUIManager : MonoBehaviour
{
    [SerializeField] private Slider bossHPSlider;
    [SerializeField] private BossBattleBossCore bossCore;
    private int bossHP;

    // Start is called before the first frame update
    void Start()
    {
        int maxHP = bossCore.bossHP;
        bossHPSlider.maxValue = maxHP;//値型にする
    }

    // Update is called once per frame
    void Update()
    {
        bossHP = bossCore.bossHP;
        bossHPSlider.value = bossHP;
    }
}
