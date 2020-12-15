using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleScaffold : MonoBehaviour
{
    [SerializeField] private Transform playerFootTrans;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("トリガー");
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot"))
        {
            Debug.Log("トリガーオフ");
            boxCollider.isTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("コリジョン");
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerFoot"))
        {
            Debug.Log("トリガーオン");
            boxCollider.isTrigger = true;
        }
    }
}
