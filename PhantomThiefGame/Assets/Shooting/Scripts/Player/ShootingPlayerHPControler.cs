using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerHPControler : MonoBehaviour
{
    [SerializeField] private int playerMaxHitPoint;
    private int playerNowHitPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerNowHitPoint=playerMaxHitPoint;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerNowHitPoint <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddDamage()
    {
        playerNowHitPoint--;
    }
}
