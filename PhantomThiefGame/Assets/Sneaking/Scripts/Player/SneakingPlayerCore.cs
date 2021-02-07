using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingPlayerCore : MonoBehaviour
{
    [Header("HP")]
    public int maxHP;
    public int nowHP;

    [Header("状態")]
    public bool isMove;
    public Vector3 moveVec;
    public bool isHide;
    public HideObjectType nowHideObjectType;
    public bool isDiscovered;
    public Transform checkPoint;

    private void Awake()
    {
        nowHP = maxHP;
    }
}
