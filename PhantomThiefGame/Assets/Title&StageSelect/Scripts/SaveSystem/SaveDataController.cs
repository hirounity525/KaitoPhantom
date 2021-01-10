using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataController : MonoBehaviour
{
    [SerializeField] private SaveDataSelecter saveDataSelecter;

    private SaveDataCore saveDataCore;

    private void Awake()
    {
        saveDataCore = GetComponent<SaveDataCore>();
    }

    public void Load()
    {
        if (!File.Exists(Application.dataPath + "/SaveData/SaveData" + saveDataCore.saveDataNum + ".json"))
        {
            SaveData newSaveData;

            newSaveData.playerName = "NEWGAME";
            newSaveData.clearStageNum = 0;

            saveDataCore.saveData = newSaveData;
            saveDataCore.isNewData = true;
            saveDataCore.isDrawUpdate = false;
            return;
        }

        StreamReader streamReader = new StreamReader(Application.dataPath + "/SaveData/SaveData" + saveDataCore.saveDataNum + ".json");
        string json = streamReader.ReadToEnd();
        streamReader.Close();

        saveDataCore.saveData = JsonUtility.FromJson<SaveData>(json);
        saveDataCore.isNewData = false;
        saveDataCore.isDrawUpdate = false;
    }

    public void Delete()
    {
        File.Delete(Application.dataPath + "/SaveData/SaveData" + saveDataCore.saveDataNum + ".json");
        Load();
    }

    public void Select()
    {
        saveDataSelecter.SelectSaveData(saveDataCore.saveDataNum, saveDataCore.saveData, saveDataCore.isNewData);
    }
}
