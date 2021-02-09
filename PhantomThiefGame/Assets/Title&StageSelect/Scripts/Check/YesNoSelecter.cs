using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoSelecter : MonoBehaviour
{
    public bool isSelect;
    public bool isYes;

    public void SelectYes()
    {
        isYes = true;
        isSelect = true;
    }

    public void SelectNo()
    {
        isYes = false;
        isSelect = true;
    }
}
