using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBulletJudgment : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameObject Enemy = other.gameObject;
        if (Enemy.tag == "Enemy")
        {
            Enemy.GetComponent<ShootingEnemyHPControler>().AddDamage();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
