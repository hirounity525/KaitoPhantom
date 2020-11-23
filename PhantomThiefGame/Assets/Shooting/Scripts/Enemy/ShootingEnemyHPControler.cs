using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyHPControler : MonoBehaviour
{
    [SerializeField] private int enemyMaxHitPoint;

    private int enemyNowHitPoint;
    // Start is called before the first frame update
    void Start()
    {
        enemyNowHitPoint = enemyMaxHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyNowHitPoint <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddDamage()
    {
        enemyNowHitPoint--;
    }
}
