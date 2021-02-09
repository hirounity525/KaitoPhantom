using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyToTarget : MonoBehaviour
{
    [SerializeField] private float moveTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        GameObject toTarget = other.gameObject;
        if (toTarget.tag == "ToTarget")
        {
            this.transform.DOMove(toTarget.transform.position, moveTime).OnComplete(() =>  AttackTarget(toTarget) ) ;

        }
    }

private void AttackTarget(GameObject targetObj)
    {
        gameObject.SetActive(false);
        targetObj.GetComponent<DefenseFriendHPControler>().AddDamage();
    }
}
