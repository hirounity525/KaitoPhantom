using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossBattleBossCore : MonoBehaviour
{
    public bool isClear; //実際にシーンなどを切り替えるときに使う
    public bool isClearTemp; //ボスバトルクリア
    public int bossHP;
    [SerializeField] private float stateWAITTime;
    [SerializeField] private float stateMOVETime;
    [SerializeField] private float stateJUMPTime;
    [SerializeField] private float stateSUMMONGUARDSTime;
    [SerializeField] private float stateSUMMONROCKTime;
    [SerializeField] private float stateGUNATTACKTime;
    [SerializeField] private float stateDYINGTime;          //ボスを倒した後何秒でisClearをtrueにさせるかの時間
    [SerializeField] private float stateDRONEATTACKTime;
    private float stateWAITTimeTemp;
    private float stateMOVETimeTemp;
    private float stateJUMPTimeTemp;
    private float stateSUMMONGUARDSTimeTemp;
    private float stateSUMMONROCKTimeTemp;
    private float stateGUNATTACKTimeTemp;
    private float stateDYINGTimeTemp;
    private float stateDRONEATTACKTimeTemp;

    [SerializeField] private BossAIState bossAIState = BossAIState.WAIT;

    [SerializeField] private BossBattleBossAttacker bossAttacker;
    private System.Random rnd = new System.Random();
    private bool oneActionFinished = false;

    private int bossAIStateNum = 6;

    [SerializeField] private BossBattleBossInfo bossInfo;

    //[SerializeField] private float blowAnimationTime =1; //この時間がたった後に警備員が呼び出される
    //[SerializeField] private float stompAnimationTime = 1.5f;//この時間がたった後に物が降ってくる
    [SerializeField] private float gunAttackAnimationTime;//この時間がたった後に銃で攻撃する
    [SerializeField] private float droneAttackAnimationTime;//この時間がたった後にドローンで攻撃する
    [SerializeField] private float droneAttackAnimationTime2;//ドローンで攻撃を始めてから攻撃アニメーションが終わるまでの時間

    public int dronePattern;

    [SerializeField] private SEPlayer sEPlayer;
    [SerializeField] private float blowSETiming;
    [SerializeField] private float blowSEToFinishBlowAnimationTime;
    [SerializeField] private float stompSETiming;
    [SerializeField] private float stompSEToFinishStompAnimationTime;

    [SerializeField] private Rigidbody rb;


    [SerializeField] private SEPlayer sEPlayer5;

    //[SerializeField] private BossBattlePlayerCore playerCore;

    public enum BossAIState
    {
        WAIT = 0,  //行動を一旦停止
        MOVE = 1,  //移動
        JUMP = 2,  //ジャンプ
        SUMMONGUARDS = 3,  //警備兵召喚
        SUMMONROCK = 4, //岩召喚
        GUNATTACK = 5, //銃攻撃
        DRONEATTACK = 6,  //ドローン攻撃
        DYING = 7 //ボスHP0以下
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear)
        {
            Debug.Log("クリアしました");
        }

        if (bossHP <= 0)//ボスのHPが0以下になったときDYING以外のStateを強制敵に外す必要があるため、それぞれのTempTimeを0とする
        {
            if (!isClear)
            {
                if (!isClearTemp)
                {
                    stateWAITTimeTemp = 0;
                    stateMOVETimeTemp = 0;
                    stateJUMPTimeTemp = 0;
                    stateSUMMONGUARDSTimeTemp = 0;
                    stateSUMMONROCKTimeTemp = 0;
                    stateGUNATTACKTimeTemp = 0;
                    stateDRONEATTACKTimeTemp = 0;
                    //oneActionFinished = true;
                    oneActionFinished = false;
                    //bossAIState = BossAIState.DYING;
                    //bossInfo.isDying = true;//倒れるアニメーション開始
                    bossInfo.isMove = false;
                    bossInfo.isJump = false;
                    bossInfo.isSummonGuards = false;
                    bossInfo.isSummonRock = false;
                    bossInfo.isGunAttack = false;
                    bossInfo.isDroneAttack = false;

                    StopCoroutine(SummonGuardsAction());
                    StopCoroutine(SummonRocksAction());
                    StopCoroutine(GunAttackAction());
                    StopCoroutine(DroneAttackAction());
                    rb.velocity = Vector3.zero;
                    bossAIState = BossAIState.DYING;
                    Debug.Log(bossAIState);
                    isClearTemp = true;
                }
            }
        }
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
                //bossAIState = BossAIState.DRONEATTACK;
                //bossAIState = BossAIState.DYING;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);//連続でWAITが来ることがある
                bossAIState += rnd.Next(1, bossAIStateNum + 1);//連続でWAITが来ないようにした(ほんの少しだけ難易度が高い)
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

        else if (bossAIState == BossAIState.DYING)
        {
            stateDYINGTimeTemp += Time.fixedDeltaTime;
            if (stateDYINGTime <= stateDYINGTimeTemp)
            {
                isClear = true;
                //stateDYINGTimeTemp = 0;
                //oneActionFinished = false;
                //bossAIState += (int)bossAIState * -1 + rnd.Next(0, bossAIStateNum + 1);
                //bossAIState = BossAIState.WAIT;
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
                    //Debug.Log(bossAIState);
                    //bossAttacker.DroneAttack();
                    StartCoroutine(DroneAttackAction());
                    oneActionFinished = true;
                }
                break;
            case BossAIState.DYING:
                if (!oneActionFinished)
                {
                    //Debug.Log(bossAIState);
                    //bossAttacker.BigBeam();
                    bossInfo.isDying = true;//倒れるアニメーション開始
                    //bossInfo.isDying = false;
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
        yield return new WaitForSeconds(blowSETiming);
        sEPlayer.Play("SummonGuards");
        yield return new WaitForSeconds(blowSEToFinishBlowAnimationTime);
        //yield return new WaitForSeconds(blowAnimationTime);
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
        //yield return new WaitForSeconds(stompAnimationTime);
        yield return new WaitForSeconds(stompSETiming);
        sEPlayer.Play("SummonRocks");
        yield return new WaitForSeconds(stompSEToFinishStompAnimationTime);
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

    private IEnumerator DroneAttackAction()
    {
        Debug.Log(bossAIState);
        dronePattern = UnityEngine.Random.Range(0, 3);
        bossInfo.isDroneAttack = true;//ドローンで攻撃するアニメーション開始
        yield return new WaitForSeconds(droneAttackAnimationTime);
        bossAttacker.DroneAttack();
        yield return new WaitForSeconds(droneAttackAnimationTime2);
        bossInfo.isDroneAttack = false;//ドローンで攻撃するアニメーション自体は終了してIdleアニメーションにする
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            sEPlayer5.Play("BossDamage");
            bossHP--;
            Debug.Log("BossHP = " + bossHP);
        }
    }
}
