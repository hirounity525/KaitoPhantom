using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttackRange : MonoBehaviour
{
    public bool isAttack = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObj = other.gameObject;

        if (hitObj.tag == "Attack")
        {
            isAttack = true;
        }
        else if (hitObj.tag == "StopAttack")
        {
            isAttack = false;
        }
    }

}
