﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField] private SaveDataController[] saveDataControllers;

    [SerializeField] private int saveDataNum;
    [SerializeField] private SaveData saveData;

    public bool isSave;

    private void Update()
    {
        if (isSave)
        {
            Save(saveDataNum, saveData);
            isSave = false;
        }
    }

    public void AllLoad()
    {
        foreach(SaveDataController saveDataController in saveDataControllers)
        {
            saveDataController.Load();
        }
    }

    public void Save(int saveDataNum, SaveData saveData)
    {
        StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/SaveData/SaveData" + saveDataNum + ".json");
        string json = JsonUtility.ToJson(saveData);
        streamWriter.Write(json);
        streamWriter.Close();
    }

    public void NewSave(int saveDataNum, string playerName)
    {
        SaveData newSaveData;

        newSaveData.clearStageNum = 0;
        newSaveData.playerName = playerName;

        Save(saveDataNum, newSaveData);
    }
}