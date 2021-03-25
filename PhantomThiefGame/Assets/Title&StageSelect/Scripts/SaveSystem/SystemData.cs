using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音量設定データ
[System.Serializable]
public struct SoundConfigData
{
    public float masterVolume;
    public float bgmVolume;
    public float seVolume;
}

//システムデータ
[System.Serializable]
public struct SystemData
{
    public SoundConfigData soundConfigData;
}
