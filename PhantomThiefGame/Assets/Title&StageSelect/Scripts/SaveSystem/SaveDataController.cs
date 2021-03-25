using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//各セーブデータへの処理
public class SaveDataController : MonoBehaviour
{
    [SerializeField] private SaveDataSelecter saveDataSelecter;

    private SaveDataCore saveDataCore;

    private void Awake()
    {
        saveDataCore = GetComponent<SaveDataCore>();
    }

    //データロード
    public void Load()
    {
        //ファイルが見つからなかったら、新しいセーブデータとする
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

        //ファイルからデータを探して、読み込む
        StreamReader streamReader = new StreamReader(Application.dataPath + "/SaveData/SaveData" + saveDataCore.saveDataNum + ".json");
        string json = streamReader.ReadToEnd();
        streamReader.Close();

        //json→SaveData
        saveDataCore.saveData = JsonUtility.FromJson<SaveData>(json);
        saveDataCore.isNewData = false;
        saveDataCore.isDrawUpdate = false;
    }

    //データ消去
    public void Delete()
    {
        File.Delete(Application.dataPath + "/SaveData/SaveData" + saveDataCore.saveDataNum + ".json");
        Load();
    }

    //選択
    public void Select()
    {
        saveDataSelecter.SelectSaveData(saveDataCore.saveDataNum, saveDataCore.saveData, saveDataCore.isNewData);
    }
}
