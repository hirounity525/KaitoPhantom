using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleDroneAppear : MonoBehaviour
{
    [SerializeField] private BossBattleBossInfo bossInfo;
    [SerializeField] private BossBattleBossCore bossCore;
    [SerializeField] private SkinnedMeshRenderer droneSkinnedMeshRenderer;
    [SerializeField] private Material blue;
    [SerializeField] private Material yellow;
    [SerializeField] private Material red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossInfo.isDroneAttack)
        {
            if (bossCore.dronePattern == 0)
            {
                droneSkinnedMeshRenderer.material = blue;
            }
            else if (bossCore.dronePattern == 1)
            {
                droneSkinnedMeshRenderer.material = yellow;
            }
            else if (bossCore.dronePattern == 2)
            {
                droneSkinnedMeshRenderer.material = red;
            }

            droneSkinnedMeshRenderer.enabled = true;
        }

        else
        {
            droneSkinnedMeshRenderer.enabled = false;
        }
    }
}
