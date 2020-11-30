using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossCore : MonoBehaviour
{
    [SerializeField] private int bossHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
