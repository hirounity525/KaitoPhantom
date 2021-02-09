using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPlayerBlinking : MonoBehaviour
{
    [SerializeField] private ScrollActionPlayerCore playerCore;
    [SerializeField] private SkinnedMeshRenderer bodySkinnedMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer faceSkinnedMeshRenderer;
    [SerializeField] private float blinkingIntervalTime;
    private float blinkingIntervalTimeTemp;
    private bool isMeshRedererEnabledTrue = true;
    private bool isFirstMeshRedererEnabledToFalse = false;//攻撃があった瞬間から点滅させるための一回使い切りのbool

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (playerCore.isInvicible)//無敵時間の間
        {
            if (!isFirstMeshRedererEnabledToFalse)//攻撃があった瞬間から点滅させるための一回使い切り
            {
                bodySkinnedMeshRenderer.enabled = false;
                faceSkinnedMeshRenderer.enabled = false;
                isMeshRedererEnabledTrue = false;
                isFirstMeshRedererEnabledToFalse = true;
            }

            if (isMeshRedererEnabledTrue)//ファントムちゃん表示中
            {
                blinkingIntervalTimeTemp += Time.fixedDeltaTime;
                if (blinkingIntervalTimeTemp >= blinkingIntervalTime)
                {
                    blinkingIntervalTimeTemp = 0;
                    bodySkinnedMeshRenderer.enabled = false;
                    faceSkinnedMeshRenderer.enabled = false;
                    isMeshRedererEnabledTrue = false;
                }
            }
            else//ファントムちゃん非表示中
            {
                blinkingIntervalTimeTemp += Time.fixedDeltaTime;
                if (blinkingIntervalTimeTemp >= blinkingIntervalTime)
                {
                    blinkingIntervalTimeTemp = 0;
                    bodySkinnedMeshRenderer.enabled = true;
                    faceSkinnedMeshRenderer.enabled = true;
                    isMeshRedererEnabledTrue = true;
                }
            }

        }
        else//無敵時間でなくなったとき
        {
            blinkingIntervalTimeTemp = 0;
            bodySkinnedMeshRenderer.enabled = true;
            faceSkinnedMeshRenderer.enabled = true;
            isFirstMeshRedererEnabledToFalse = false;
        }
    }
}
