using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingEnemyManager : MonoBehaviour
{
    public bool isPlayerDiscovery;

    [SerializeField] private SneakingEnemyCore[] enemyCores;

    // Update is called once per frame
    void Update()
    {
        foreach(SneakingEnemyCore enemyCore in enemyCores)
        {
            if (enemyCore.isDiscovery)
            {
                isPlayerDiscovery = true;
            }
        }
    }
}
