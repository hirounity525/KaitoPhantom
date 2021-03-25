using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//セーブデータ選択画面でのデータの表示
public class SaveDataDrawer : MonoBehaviour
{
    [SerializeField] private SaveDataCore saveDataCore;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI progressText;

    // Update is called once per frame
    void Update()
    {
        if (!saveDataCore.isDrawUpdate)
        {
            nameText.text = saveDataCore.saveData.playerName;

            //クリア数→％表記
            int progressPercentage = (int)(((float)saveDataCore.saveData.clearStageNum / CommonData.Instance.maxStageNum) * 100);
            progressText.text = progressPercentage.ToString();

            saveDataCore.isDrawUpdate = true;
        }
    }
}
