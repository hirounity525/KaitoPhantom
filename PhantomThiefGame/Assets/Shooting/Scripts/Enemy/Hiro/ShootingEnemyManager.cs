﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyObjs;

    private ShootingEnemyManagerCore enemyManagerCore;

    private bool isAllDestroy;

    private void Awake()
    {
        enemyManagerCore = GetComponent<ShootingEnemyManagerCore>();
    }

    private void Update()
    {
        if (!isAllDestroy)
        {
            if (enemyManagerCore.enemyDestroyNum == enemyObjs.Length)
            {
                isAllDestroy = true;
            }
        }
    }

    public void DisableEnemies()
    {
        foreach (GameObject enemyObj in enemyObjs)
        {
            enemyObj.SetActive(false);
        }
    }

    public void EnableEnemies()
    {
        foreach(GameObject enemyObj in enemyObjs)
        {
            enemyObj.SetActive(true);
        }
    }

    public bool IsAllDestroy()
    {
        return isAllDestroy;
    }
}
