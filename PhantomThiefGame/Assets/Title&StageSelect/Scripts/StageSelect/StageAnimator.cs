using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 scaleUpVec;
    [SerializeField] private float animTime;

    private StageCore stageCore;
    private Transform stageTrans;

    private bool isEnlargeAnim, isShrinkAnim;

    private void Awake()
    {
        stageCore = GetComponent<StageCore>();
        stageTrans = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stageCore.isViewed)
        {
            if (!isEnlargeAnim)
            {
                isShrinkAnim = false;

                stageTrans.DOScale(scaleUpVec, animTime);

                isEnlargeAnim = true;
            }
        }
        else
        {
            if (!isShrinkAnim)
            {
                isEnlargeAnim = false;

                stageTrans.DOScale(Vector3.one, animTime);

                isShrinkAnim = true;
            }
        }
    }
}
