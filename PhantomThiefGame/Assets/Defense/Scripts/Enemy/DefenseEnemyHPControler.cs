using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyHPControler : MonoBehaviour
{
    private int hitPoints;
    [SerializeField]private int maxHitPoints;
    // Start is called before the first frame update
    void OnEnable()
    {
        hitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddDamage()
    {
        hitPoints--;
    }
}
