using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayerCore : MonoBehaviour
{
    public bool isRight;
    private DefensePlayerAttacker playerAttacker;
    public bool canMove;

    void Start()
    {
        playerAttacker = GetComponent<DefensePlayerAttacker>();
    }

    void Update()
    {
        canMove = playerAttacker.canShoot;
    }
}
