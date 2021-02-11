using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeneseLifeManager : MonoBehaviour
{
    [SerializeField] private DefenseFriendHPControler friendHPControler;
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
        for (int i = 0; i < friendHPControler.maxHitpoints; i++)
        {
            GameObject life = Instantiate(lifeObj, lifesTrans);
            lifeControllerList.Add(life.GetComponent<LifeController>());
        }

        nowHP = friendHPControler.maxHitpoints;
    }

    private void Update()
    {
        if (friendHPControler.hitPoints < nowHP)
        {
            lifeControllerList[nowHP - 1].DeleteLife();

            nowHP = friendHPControler.hitPoints;
        }
    }
}
