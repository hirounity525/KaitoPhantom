using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private string playerName;

    [SerializeField] private TitleInputProvider titleInput;

    private void Update()
    {
        if (titleInput.isCancelButtonDown)
        {
            if(playerName.Length != 0)
            {
                playerName = playerName.Remove(playerName.Length - 1, 1);
            }
        }

        playerNameText.text = playerName;
    }

    public void SetSyllabary(string syllabary)
    {
        playerName += syllabary;
    }
}
