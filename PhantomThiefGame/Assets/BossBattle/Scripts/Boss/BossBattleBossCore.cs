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
    [SerializeField] private float stateBIGBEAMTime;
    [SerializeField] private float stateDRONEATTACKTime;
    private float stateWAITTimeTemp;
    private float stateMOVETimeTemp;
    private float stateJUMPTimeTemp;
    private float stateSUMMONGUARDSTimeTemp;
    private float stateSUMMONROCKTimeTemp;
    private float stateGUNATTACKTimeTemp;
    private float stateBIGBEAMTimeTemp;
    private float stateDRONEATTACKTimeTemp;

    [SerializeField] private BossAIState bossAIState = BossAIState.WAIT;

    [SerializeField] private BossBattleBossAttacker bossAttacker;
    private System.Random rnd = new System.Random();
    private bool oneActionFinished = false;

    private int bossAIStateNum = 6;

    [SerializeField] private BossBattleBossInfo bossInfo;

    [SerializeField] private float blowAnimationTime; //この時間がたった後に警備員が呼び出される
    [SerializeField] private float stompAnimationTime;//この時間がたった後に物が降ってくる
    [SerializeField] private float gunAttackAnimationTime;//この時間がたった後に銃で攻撃する

    public enum BossAIState
    {
        WAIT = 0,  //行動を一旦停止
        MOVE = 1,  //移動
        JUMP = 2,  //ジャンプ
        SUMMONGUARDS = 3,  //警備兵召喚
        SUMMONROCK = 4, //岩召喚
        GUNATTACK = 5, //銃攻撃
        DRONEATTACK = 6,  //ドローン攻撃
        BIGBEAM = 7 //大きいビーム
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
        //Debug.Log(bossAIState);
        if(bossAIState == BossAIState.WAIT)
        {
            stateWAITTimeTemp += Time.fixedDeltaTime;
            if(stateWAITTime <= stateWAITTimeTemp)
            {
                stateWAITTimeTemp = 0;
                oneActionFinished = false;
                //bossAIState = BossAIState.MOVE;
                //bossAIState = BossAIState.JUMP;
                //bossAIState = BossAIState.SUMMONGUARDS;
                //bossAIState = BossAIState.SUMMONROCK;
                //bossAIState = BossAIState.GUNATTACK;
                bossAIState = BossAIState.DRONEATTACK;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
            }
        }

        else if (bossAIState == BossAIState.MOVE)
        {
            stateMOVETimeTemp += Time.fixedDeltaTime;

            if (stateMOVETime <= stateMOVETimeTemp)
            {
                stateMOVETimeTemp = 0;
                oneActionFinished = false;
                bossInfo.isMove = false;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
            }
        }

        else if (bossAIState == BossAIState.JUMP)
        {
            stateJUMPTimeTemp += Time.fixedDeltaTime;
            if (stateJUMPTime <= stateJUMPTimeTemp)
            {
                stateJUMPTimeTemp = 0;
                oneActionFinished = false;
                bossInfo.isJump = false;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
            }
        }

        else if (bossAIState == BossAIState.SUMMONGUARDS)
        {
            stateSUMMONGUARDSTimeTemp += Time.fixedDeltaTime;
            if (stateSUMMONGUARDSTime <= stateSUMMONGUARDSTimeTemp)
            {
                stateSUMMONGUARDSTimeTemp = 0;
                oneActionFinished = false;
                //bossInfo.isSummonGuards = false;//笛を吹くアニメーション終了
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
            }
        }

        else if (bossAIState == BossAIState.SUMMONROCK)
        {
            stateSUMMONROCKTimeTemp += Time.fixedDeltaTime;
            if (stateSUMMONROCKTime <= stateSUMMONROCKTimeTemp)
            {
                stateSUMMONROCKTimeTemp = 0;
                oneActionFinished = false;
                //bossInfo.isSummonRock = false;//地団駄を踏むアニメーション終了
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
            }
        }

        else if (bossAIState == BossAIState.GUNATTACK)
        {
            stateGUNATTACKTimeTemp += Time.fixedDeltaTime;
            if (stateGUNATTACKTime <= stateGUNATTACKTimeTemp)
            {
                stateGUNATTACKTimeTemp = 0;
                oneActionFinished = false;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
            }
        }

        else if (bossAIState == BossAIState.DRONEATTACK)
        {
            stateDRONEATTACKTimeTemp += Time.fixedDeltaTime;
            if (stateDRONEATTACKTime <= stateDRONEATTACKTimeTemp)
            {
                stateDRONEATTACKTimeTemp = 0;
                oneActionFinished = false;
                //bossAIState = BossAIState.DRONEATTACK;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
            }
        }

        else if (bossAIState == BossAIState.BIGBEAM)
        {
            stateBIGBEAMTimeTemp += Time.fixedDeltaTime;
            if (stateBIGBEAMTime <= stateBIGBEAMTimeTemp)
            {
                stateBIGBEAMTimeTemp = 0;
                oneActionFinished = false;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                bossAIState = BossAIState.WAIT;
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
                    bossInfo.isMove = true;
                    oneActionFinished = true;
                }
                break;
            case BossAIState.JUMP:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    bossAttacker.BossJump();
                    bossInfo.isJump = true;
                    oneActionFinished = true;
                }
                break;
            case BossAIState.SUMMONGUARDS:
                if (!oneActionFinished)
                {
                    StartCoroutine(SummonGuardsAction());
                    oneActionFinished = true;
                }
                break;
            case BossAIState.SUMMONROCK:
                if (!oneActionFinished)
                {
                    StartCoroutine(SummonRocksAction());
                    oneActionFinished = true;
                }
                break;
            case BossAIState.GUNATTACK:
                if (!oneActionFinished)
                {
                    StartCoroutine(GunAttackAction());
                    oneActionFinished = true;
                }
                break;
            case BossAIState.DRONEATTACK:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    bossAttacker.DroneAttack();
                    oneActionFinished = true;
                }
                break;
            case BossAIState.BIGBEAM:
                if (!oneActionFinished)
                {
                    Debug.Log(bossAIState);
                    bossAttacker.BigBeam();
                    oneActionFinished = true;
                }
                break;
            
        }
    }

    private IEnumerator SummonGuardsAction()
    {
        int i = 0;
        Debug.Log(bossAIState);
        bossInfo.isSummonGuards = true; //笛を吹くアニメーション開始
        yield return new WaitForSeconds(blowAnimationTime);
        bossInfo.isSummonGuards = false;//笛を吹くアニメーション自体は終了してIdleアニメーションにする
        while (i < rnd.Next(3, 5))//3個か4個召喚
        {
            bossAttacker.SummonGuards();
            i++;
        }
        i = 0;
    }

    private IEnumerator SummonRocksAction()
    {
        int i = 0;
        Debug.Log(bossAIState);
        bossInfo.isSummonRock = true; //地団駄を踏むアニメーション開始
        yield return new WaitForSeconds(stompAnimationTime);
        bossInfo.isSummonRock = false;//地団駄を踏むアニメーション自体は終了してIdleアニメーションにする
        while (i < rnd.Next(3, 5))//3個か4個召喚
        {
            bossAttacker.SummonRock();
            i++;
        }
        i = 0;
    }

    private IEnumerator GunAttackAction()
    {
        Debug.Log(bossAIState);
        bossInfo.isGunAttack = true;//銃を撃つアニメーション開始
        yield return new WaitForSeconds(gunAttackAnimationTime);
        bossInfo.isGunAttack = false;//銃を撃つアニメーション自体は終了してIdleアニメーションにする
        bossAttacker.GunAttack();
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
