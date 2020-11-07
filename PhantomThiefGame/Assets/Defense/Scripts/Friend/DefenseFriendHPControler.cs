using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseFriendHPControler : MonoBehaviour
{
    [SerializeField] private int maxHitpoints;
    private int hitPoints;
    // Start is called before the first frame update
    private void OnEnable()
    {
        hitPoints = maxHitpoints;
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
