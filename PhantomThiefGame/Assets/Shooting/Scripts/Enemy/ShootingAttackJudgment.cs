using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttackJudgment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        GameObject Player = other.gameObject;
        if (Player.tag == "Player")
        {
            Player.GetComponent<ShootingPlayerHPControler>().AddDamage();
            gameObject.SetActive(false);
        }
    }
}
