using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttackRange : MonoBehaviour
{
    public Collider co;
    public bool isAttack=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        GameObject Enemy = other.gameObject;
        if (Enemy.tag == "Enemy")
        {
            isAttack = true;
        }
    }
}
