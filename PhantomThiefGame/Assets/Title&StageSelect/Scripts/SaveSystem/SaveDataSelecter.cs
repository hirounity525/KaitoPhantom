using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataSelecter : MonoBehaviour
{
    public bool isSelect;

    public void SelectSaveData(int saveDataNum, SaveData saveData, bool isNewData)
    {
        CommonData.Instance.selectSaveDataNum = saveDataNum;
        CommonData.Instance.selectSaveData = saveData;
        CommonData.Instance.isSelectNewData = isNewData;

        isSelect = true;
    }
}
