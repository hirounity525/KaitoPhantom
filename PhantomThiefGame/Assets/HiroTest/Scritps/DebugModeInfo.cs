using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugModeInfo : SingletonMonoBehaviour<DebugModeInfo>
{
    public bool isTtoSSMode;
    public bool isBackTitle;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
