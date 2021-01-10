using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameSetter : MonoBehaviour
{
    public string playerName;

    public bool isDecide;

    [SerializeField] private int maxNameLength;

    [SerializeField] private TitleInputProvider titleInput;

    private void Update()
    {
        if (titleInput.isCancelButtonDown)
        {
            RemoveOneCharacter();
        }
    }

    public void SetSyllabary(string syllabary)
    {
        if(playerName.Length != maxNameLength)
        {
            playerName += syllabary;
        }
    }

    public void RemoveOneCharacter()
    {
        if (playerName.Length != 0)
        {
            playerName = playerName.Remove(playerName.Length - 1, 1);
        }
    }

    public void Decide()
    {
        if(playerName != "")
        {
            isDecide = true;
        }
    }

    public void ResetName()
    {
        playerName = "";
    }
}
