﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : SingletonMonoBehaviour<CommonData>
{
    public int maxStageNum;

    [Header("SaveData")]
    public int selectSaveDataNum;
    public SaveData selectSaveData;
    public bool isSelectNewData;

    [Header("StageInfo")]
    public int selectedStageNum;
    public bool isClear;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
