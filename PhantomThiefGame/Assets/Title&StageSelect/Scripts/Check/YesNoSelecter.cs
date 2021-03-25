using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//はい・いいえの選択処理
public class YesNoSelecter : MonoBehaviour
{
    public bool isSelect;
    public bool isYes;

    //「はい」を選んだら
    public void SelectYes()
    {
        isYes = true;
        isSelect = true;
    }

    //「いいえ」を選んだら
    public void SelectNo()
    {
        isYes = false;
        isSelect = true;
    }
}
