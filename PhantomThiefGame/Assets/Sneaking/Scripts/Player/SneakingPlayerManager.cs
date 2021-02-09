using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingPlayerManager : MonoBehaviour
{
    private SneakingPlayerCore playerCore;
    private Transform playerTrans;

    private void Awake()
    {
        playerCore = GetComponent<SneakingPlayerCore>();
        playerTrans = GetComponent<Transform>();
    }

    public void AddDamage()
    {
        playerCore.isDiscovered = true;
        playerCore.nowHP--;
    }

    public int NowHP()
    {
        return playerCore.nowHP;
    }

    public void ResetPlayer()
    {
        playerTrans.position = playerCore.checkPoint.position;
        playerTrans.rotation = Quaternion.identity;

        playerCore.isDiscovered = false;
    }

    public bool IsClear()
    {
        return playerCore.isClear;
    }
}
