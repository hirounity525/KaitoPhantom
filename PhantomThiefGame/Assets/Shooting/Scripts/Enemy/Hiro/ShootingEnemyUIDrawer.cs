using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingEnemyUIDrawer : MonoBehaviour
{
    [SerializeField] private ShootingEnemyManagerCore enemyManagerCore;

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int remainNum = enemyManagerCore.enemyNum - enemyManagerCore.enemyDestroyNum;
        textMeshPro.text = remainNum.ToString();
    }

}
