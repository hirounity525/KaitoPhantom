using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleGunAppear : MonoBehaviour
{
    [SerializeField] private BossBattleBossInfo bossInfo;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossInfo.isGunAttack)
        {
            meshRenderer.enabled = true;
        }

        else
        {
            meshRenderer.enabled = false;
        }
    }
}
