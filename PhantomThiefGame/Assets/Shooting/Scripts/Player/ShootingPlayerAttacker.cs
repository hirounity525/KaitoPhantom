﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerAttacker : MonoBehaviour
{
    [SerializeField]private ShootingInputProvider inputProvider;
    [SerializeField]private ObjectPool bulletPool;
    [SerializeField] private Transform gunNozzlePos;

    private bool isAttack=false;

    [SerializeField] private float attackWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAttack)
        {
            if (inputProvider.isAttackButtunDown > 0)
            {
                GameObject bullet = bulletPool.GetObject();
                bullet.transform.position = gunNozzlePos.position;
                StartCoroutine(AttackWaitTime());
            }
        }

    }

    private IEnumerator AttackWaitTime()
    {
        isAttack = true;

        yield return new WaitForSeconds(attackWaitTime);

        isAttack = false;
    }
}
