using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField] private SaveData saveData;
    [SerializeField] private SaveData loadData;
    [SerializeField] private int saveDataNum;
    public bool isSave;
    public bool isLoad;

    private void Update()
    {
        if (isSave)
        {
            Save(saveDataNum);
            isSave = false;
        }

        if (isLoad)
        {
            Load(saveDataNum);
            isLoad = false;
        }
    }

    public void Save(int saveDataNum)
    {
        StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/SaveData/SaveData" +saveDataNum+ ".json");
        string json = JsonUtility.ToJson(saveData);
        streamWriter.Write(json);
        streamWriter.Close();
    }

    public void Load(int saveDataNum)
    {
        if (!File.Exists(Application.dataPath + "/SaveData/SaveData" + saveDataNum + ".json")){
            Debug.Log("ありません");
            return;
        }

        StreamReader streamReader = new StreamReader(Application.dataPath + "/SaveData/SaveData" +saveDataNum+ ".json");
        string json = streamReader.ReadToEnd();
        streamReader.Close();

        loadData = JsonUtility.FromJson<SaveData>(json);
    }
}
