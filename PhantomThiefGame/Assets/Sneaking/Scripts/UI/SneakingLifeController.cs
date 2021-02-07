using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingLifeController : MonoBehaviour
{
    [SerializeField] private SneakingPlayerCore playerCore;
    [SerializeField] private GameObject lifeObj;
    [SerializeField] private float lifeDeleteTime;

    private Transform lifesTrans;

    private List<SneakingLifeAnimatior> lifeAnimList = new List<SneakingLifeAnimatior>();
    private int nowHP;

    private void Awake()
    {
        lifesTrans = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerCore.maxHP; i++)
        {
            GameObject life = Instantiate(lifeObj, lifesTrans);
            lifeAnimList.Add(life.GetComponent<SneakingLifeAnimatior>());
        }

        nowHP = playerCore.maxHP;
    }

    public void DeleteLife()
    {
        lifeAnimList[nowHP - 1].PlayDeleteAnim(lifeDeleteTime);
        nowHP--;
    }

    public float LifeDeleteTime()
    {
        return lifeDeleteTime;
    }
}
