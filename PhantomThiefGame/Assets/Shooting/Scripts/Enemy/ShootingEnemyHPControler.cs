﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyHPControler : MonoBehaviour
{
    [SerializeField] private ShootingEnemyManagerCore enemyManagerCore;
    [SerializeField] private SEPlayer sePlayer;
    [SerializeField] private int enemyMaxHitPoint;

    [SerializeField] private int enemyNowHitPoint;

    private bool isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        enemyNowHitPoint = enemyMaxHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroy)
        {
            if (enemyNowHitPoint <= 0)
            {
                enemyManagerCore.enemyDestroyNum++;
                gameObject.SetActive(false);
                sePlayer.Play("Break");

                isDestroy = true;
            }
        }
    }

    public void AddDamage()
    {
        enemyNowHitPoint--;
    }
}
