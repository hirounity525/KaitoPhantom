using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataSelecter : MonoBehaviour
{
    public bool isSelect;

    public int selectSaveDataNum;
    public SaveData selectSaveData;
    public bool isSelectNewData;

    public void SelectSaveData(int saveDataNum, SaveData saveData, bool isNewData)
    {
        selectSaveDataNum = saveDataNum;
        selectSaveData = saveData;
        isSelectNewData = isNewData;

        isSelect = true;
    }
}
