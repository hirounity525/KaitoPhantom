using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossCore : MonoBehaviour
{
    [SerializeField] private int bossHP;
    [SerializeField] private float stateWAITTime;
    [SerializeField] private float stateMOVETime;
    [SerializeField] private float stateJUMPTime;
    [SerializeField] private float stateSUMMONGUARDSTime;
    [SerializeField] private float stateSUMMONROCKTime;
    [SerializeField] private float stateGUNATTACKTime;
    private float stateWAITTimeTemp;
    private float stateMOVETimeTemp;
    private float stateJUMPTimeTemp;
    private float stateSUMMONGUARDSTimeTemp;
    private float stateSUMMONROCKTimeTemp;
    private float stateGUNATTACKTimeTemp;

    [SerializeField] private BossAIState bossAIState = BossAIState.WAIT;

    [SerializeField] private BossBattleBossAttacker bossAttacker;
    private System.Random rnd = new System.Random();
    private bool oneActionFinished = false;

    public enum BossAIState
    {
        WAIT = 0,  //行動を一旦停止
        MOVE = 1,  //移動
        JUMP = 2,  //ジャンプ
        SUMMONGUARDS = 3,  //警備兵召喚
        SUMMONROCK = 4, //岩召喚
        GUNATTACK = 5 //銃攻撃
    }

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        int i = 0;
        //Debug.Log(bossAIState);
        if(bossAIState == BossAIState.WAIT)
        {
            stateWAITTimeTemp += Time.fixedDeltaTime;
            if(stateWAITTime <= stateWAITTimeTemp)
            {
                stateWAITTimeTemp = 0;
                oneActionFinished = false;
                bossAIState += (int)bossAIState * -1 + rnd.Next(0, 6);
            }
        }

        else if (bossAIState == BossAIState.MOVE)
        {
            stateMOVETimeTemp += Time.fixedDeltaTime;
            if (stateMOVETime <= stateMOVETimeTemp)
            {
                stateMOVETimeTemp = 0;
                oneActionFinished = false;
                bossAIState += (int)bossAIState * -1 + rnd.Next(0, 6);
            }
        }

        else if (bossAIState == BossAIState.JUMP)
        {
            stateJUMPTimeTemp += Time.fixedDeltaTime;
            if (stateJUMPTime <= stateJUMPTimeTemp)
            {
                stateJUMPTimeTemp = 0;
                oneActionFinished = false;
                bossAIState += (int)bossAIState * -1 + rnd.Next(0, 6);
            }
        }

        else if (bossAIState == BossAIState.SUMMONGUARDS)
        {
            stateSUMMONGUARDSTimeTemp += Time.fixedDeltaTime;
            if (stateSUMMONGUARDSTime <= stateSUMMONGUARDSTimeTemp)
            {
                stateSUMMONGUARDSTimeTemp = 0;
                oneActionFinished = false;
                bossAIState += (int)bossAIState * -1 + rnd.Next(0, 6);
            }
        }

        else if (bossAIState == BossAIState.SUMMONROCK)
        {
            stateSUMMONROCKTimeTemp += Time.fixedDeltaTime;
            if (stateSUMMONROCKTime <= stateSUMMONROCKTimeTemp)
            {
                stateSUMMONROCKTimeTemp = 0;
                oneActionFinished = false;
                bossAIState += (int)bossAIState * -1 + rnd.Next(0, 6);
            }
        }

        else if (bossAIState == BossAIState.GUNATTACK)
        {
            stateGUNATTACKTimeTemp += Time.fixedDeltaTime;
            if (stateGUNATTACKTime <= stateGUNATTACKTimeTemp)
            {
                stateGUNATTACKTimeTemp = 0;
                oneActionFinished = false;
                bossAIState += (int)bossAIState * -1 + rnd.Next(0, 6);
            }
        }

        switch (bossAIState)
        {
            case BossAIState.WAIT:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    oneActionFinished = true;
                }
                break;
            case BossAIState.MOVE:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    bossAttacker.BossMove();
                    oneActionFinished = true;
                }
                break;
            case BossAIState.JUMP:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    bossAttacker.BossJump();
                    oneActionFinished = true;
                }
                break;
            case BossAIState.SUMMONGUARDS:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    while (i < rnd.Next(3, 5))//3人か4人召喚
                    {
                        bossAttacker.SummonGuards();
                        i++;
                    }
                    i = 0;
                    oneActionFinished = true;
                }
                break;
            case BossAIState.SUMMONROCK:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    while (i < rnd.Next(3, 5))//3個か4個召喚
                    {
                        bossAttacker.SummonRock();
                        i++;
                    }
                    i = 0;
                    oneActionFinished = true;
                }
                break;
            case BossAIState.GUNATTACK:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    bossAttacker.GunAttack();
                    oneActionFinished = true;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            bossHP--;
            Debug.Log("BossHP = " + bossHP);
        }
    }
}
