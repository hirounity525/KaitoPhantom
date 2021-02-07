using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingEnemyManager : MonoBehaviour
{
    public bool isPlayerDiscovery;
    public GameObject discoverdLihgtVC;

    [SerializeField] private SneakingEnemyCore[] enemyCores;

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDiscovery)
        {
            foreach (SneakingEnemyCore enemyCore in enemyCores)
            {
                if (enemyCore.isDiscovery)
                {
                    discoverdLihgtVC = enemyCore.lightVC;
                    isPlayerDiscovery = true;
                }
            }
        }
    }

    public void ResetEnemies()
    {
        foreach (SneakingEnemyCore enemyCore in enemyCores)
        {
            enemyCore.isDiscovery = false;
            enemyCore.isReset = true;
        }
    }
}
