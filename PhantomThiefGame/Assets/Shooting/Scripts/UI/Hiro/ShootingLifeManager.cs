using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLifeManager : MonoBehaviour
{
    [SerializeField] private ShootingPlayerHPControler playerHPControler;
    [SerializeField] private GameObject lifeObj;

    private Transform lifesTrans;

    private List<LifeController> lifeControllerList = new List<LifeController>();
    private int nowHP;

    private void Awake()
    {
        lifesTrans = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerHPControler.playerMaxHitPoint; i++)
        {
            GameObject life = Instantiate(lifeObj, lifesTrans);
            lifeControllerList.Add(life.GetComponent<LifeController>());
        }

        nowHP = playerHPControler.playerMaxHitPoint;
    }

    private void Update()
    {
        if (playerHPControler.playerNowHitPoint < nowHP)
        {
            lifeControllerList[nowHP - 1].DeleteLife();

            nowHP = playerHPControler.playerNowHitPoint;
        }
    }
}
