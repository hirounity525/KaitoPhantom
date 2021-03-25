using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//セーブデータ選択処理
public class SaveDataSelecter : MonoBehaviour
{
    public bool isSelect;

    //セーブデータ選択
    public void SelectSaveData(int saveDataNum, SaveData saveData, bool isNewData)
    {
        CommonData.Instance.selectSaveDataNum = saveDataNum;
        CommonData.Instance.selectSaveData = saveData;
        CommonData.Instance.isSelectNewData = isNewData;

        isSelect = true;
    }
}
