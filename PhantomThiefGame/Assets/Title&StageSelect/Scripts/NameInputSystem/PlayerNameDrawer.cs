using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//プレイヤーの名前をTextで表示させる
public class PlayerNameDrawer : MonoBehaviour
{
    [SerializeField] private PlayerNameSetter playerNameSetter;

    private TextMeshProUGUI nameText;

    private void Awake()
    {
        nameText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = playerNameSetter.playerName;
    }
}
