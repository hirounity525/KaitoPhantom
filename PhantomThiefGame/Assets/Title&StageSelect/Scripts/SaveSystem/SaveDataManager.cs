using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//セーブデータ全体を扱う
public class SaveDataManager : MonoBehaviour
{
    [SerializeField] private SaveDataController[] saveDataControllers;

    [SerializeField] private int saveDataNum;
    [SerializeField] private SaveData saveData;

    //すべてのセーブデータをロードさせる
    public void AllLoad()
    {
        foreach(SaveDataController saveDataController in saveDataControllers)
        {
            saveDataController.Load();
        }
    }

    //データセーブ
    public void Save(int saveDataNum, SaveData saveData)
    {
        StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/SaveData/SaveData" + saveDataNum + ".json");
        string json = JsonUtility.ToJson(saveData);
        streamWriter.Write(json);
        streamWriter.Close();
    }

    //新しいセーブデータを作って、セーブする
    public void NewSave(int saveDataNum, string playerName)
    {
        SaveData newSaveData;

        newSaveData.clearStageNum = 0;
        newSaveData.playerName = playerName;

        CommonData.Instance.selectSaveData = newSaveData;

        Save(saveDataNum, newSaveData);
    }

    //クリアしたデータを作って、セーブする
    public void AllClearSave(int saveDataNum, string playerName)
    {
        SaveData newSaveData;

        newSaveData.clearStageNum = CommonData.Instance.maxStageNum;
        newSaveData.playerName = playerName;

        CommonData.Instance.selectSaveData = newSaveData;

        Save(saveDataNum, newSaveData);
    }
}
