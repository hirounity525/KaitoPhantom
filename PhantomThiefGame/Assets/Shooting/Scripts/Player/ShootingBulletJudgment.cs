using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBulletJudgment : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObj = other.gameObject;

        if (hitObj.tag == "Enemy")
        {
            hitObj.GetComponent<ShootingEnemyHPControler>().AddDamage();
            gameObject.SetActive(false);
        }
        else if (hitObj.tag == "LimitAttackLine")
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
