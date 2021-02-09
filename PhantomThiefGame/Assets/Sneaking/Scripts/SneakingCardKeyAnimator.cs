using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingCardKeyAnimator : MonoBehaviour
{
    [SerializeField] private float rotateTime;

    [SerializeField] private float floatDistance;
    [SerializeField] private float floatTime;

    private Transform objTrans;

    private void Awake()
    {
        objTrans = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        objTrans.DORotate(Vector3.up * -360, rotateTime,RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart);
        objTrans.DOMoveY(floatDistance, floatTime).SetLoops(-1, LoopType.Yoyo);
    }
}
