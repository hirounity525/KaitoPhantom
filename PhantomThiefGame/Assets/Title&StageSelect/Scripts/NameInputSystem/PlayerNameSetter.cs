using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//名前入力処理
public class PlayerNameSetter : MonoBehaviour
{
    public string playerName;

    public bool isDecide;

    [SerializeField,Tooltip("最大の名前数")] private int maxNameLength;

    [SerializeField] private TitleInputProvider titleInput;

    //1文字入力
    public void SetSyllabary(string syllabary)
    {
        if(playerName.Length != maxNameLength)
        {
            playerName += syllabary;
        }
    }

    //1文字消す
    public void RemoveOneCharacter()
    {
        if (playerName.Length != 0)
        {
            playerName = playerName.Remove(playerName.Length - 1, 1);
        }
    }

    //決定
    public void Decide()
    {
        if(playerName != "")
        {
            isDecide = true;
        }
    }

    //名前をリセットする
    public void ResetName()
    {
        playerName = "";
    }
}
