using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionBladeFromBelowMover : MonoBehaviour
{
    [Header("速度")]
    [SerializeField, Tooltip("押す速度")] private float pushSpeed;
    [SerializeField, Tooltip("引く速度")] private float pullSpeed;

    [Header("時間")]
    [SerializeField, Tooltip("押すまでの時間")] private float appearanceStartTime;
    [SerializeField, Tooltip("引くまでの時間")] private float disappearanceStartTime;

    [Header("距離")]
    [SerializeField, Tooltip("頂点のTransform")] private Transform topTrans;
    [SerializeField, Tooltip("どこまで押すかの距離")] private float pushDistance;
    private Rigidbody rb;

    private float firstPosY;
    private float pushLimitPoint;

    public bool isPush;
    public bool isPull;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        firstPosY = topTrans.position.y;
        pushLimitPoint = firstPosY + pushDistance;

        StartCoroutine(BottomWait());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPush)
        {
            if (topTrans.position.y >= pushLimitPoint)
            {
                StartCoroutine(TopWait());
            }
        }

        if (isPull)
        {
            if (topTrans.position.y <= firstPosY)
            {
                StartCoroutine(BottomWait());
            }
        }
    }

    IEnumerator TopWait()
    {
        isPush = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(disappearanceStartTime);

        rb.velocity = Vector3.down * pullSpeed;
        isPull = true;
    }

    IEnumerator BottomWait()
    {
        isPull = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(appearanceStartTime);

        rb.velocity = Vector3.up * pushSpeed;
        isPush = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topTrans.position, topTrans.position + (Vector3.up * pushDistance));
    }
}