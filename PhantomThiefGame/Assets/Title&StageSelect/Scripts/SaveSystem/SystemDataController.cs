using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SystemDataController : MonoBehaviour
{
    public SystemData systemSaveData;

    //セーブ
    public void Save(SoundConfigData soundConfigData)
    {
        systemSaveData.soundConfigData = soundConfigData;

        StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/SaveData/SystemData.json");
        string json = JsonUtility.ToJson(systemSaveData);
        streamWriter.Write(json);
        streamWriter.Close();
    }

    //ロード
    public void Load()
    {
        //ファイルが見つからなかったら、新しいセーブデータとする
        if (!File.Exists(Application.dataPath + "/SaveData/SystemData.json"))
        {
            SystemData newSystemSaveData;

            newSystemSaveData.soundConfigData.masterVolume = 1.0f;
            newSystemSaveData.soundConfigData.bgmVolume = 1.0f;
            newSystemSaveData.soundConfigData.seVolume = 1.0f;

            systemSaveData = newSystemSaveData;

            return;
        }

        //ファイルからデータを探して、読み込む
        StreamReader streamReader = new StreamReader(Application.dataPath + "/SaveData/SystemData.json");
        string json = streamReader.ReadToEnd();
        streamReader.Close();

        //json→SystemData
        systemSaveData = JsonUtility.FromJson<SystemData>(json);
    }
}
