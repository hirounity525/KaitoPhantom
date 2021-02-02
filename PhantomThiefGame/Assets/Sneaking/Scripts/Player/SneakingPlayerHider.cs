using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingPlayerHider : MonoBehaviour
{
    [SerializeField] private SneakingInputProvider inputProvider;

    [SerializeField] private float hideTime;

    [SerializeField] private GameObject mainVC;

    private SneakingPlayerCore playerCore;
    private Transform playerTrans;
    private Collider coll;
    private Rigidbody rb;

    private bool isHide;
    private bool canHide;

    private bool isMotion;

    private GameObject nowHideVC;

    private HideObjectType nowHideObjectType;
    private Vector3 hidePoint;
    private Quaternion hideRotation;

    private Vector3 beforeHidePoint;
    private Quaternion beforeHideRotation;


    private void Awake()
    {
        playerCore = GetComponent<SneakingPlayerCore>();
        playerTrans = GetComponent<Transform>();
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMotion)
        {
            if (!isHide)
            {
                if (canHide)
                {
                    if (inputProvider.isHideButtonDown)
                    {
                        coll.enabled = false;
                        rb.useGravity = false;
                        rb.velocity = Vector3.zero;

                        beforeHidePoint = playerTrans.position;
                        playerTrans.DOMoveX(hidePoint.x, hideTime);
                        playerTrans.DOMoveZ(hidePoint.z, hideTime);

                        beforeHideRotation = playerTrans.rotation;
                        playerTrans.DORotate(hideRotation.eulerAngles, hideTime);

                        mainVC.SetActive(false);
                        nowHideVC.SetActive(true);

                        isHide = true;

                        StartCoroutine(StartMotion());
                    }
                }
            }
            else
            {
                if (inputProvider.isHideButtonDown)
                {
                    coll.enabled = true;
                    rb.useGravity = true;

                    playerTrans.DOMove(beforeHidePoint, hideTime);
                    playerTrans.DORotate(beforeHideRotation.eulerAngles, hideTime);

                    mainVC.SetActive(true);
                    nowHideVC.SetActive(false);

                    isHide = false;

                    StartCoroutine(StartMotion());
                }
            }
        }

        playerCore.isHide = isHide;
    }

    private void OnTriggerEnter(Collider other)
    {
        SneakingHideObjectInfo hideObjectInfo = other.GetComponent<SneakingHideObjectInfo>();

        if(hideObjectInfo != null)
        {
            canHide = true;
            hidePoint = other.transform.position;
            hideRotation = other.transform.root.rotation;

            nowHideObjectType = hideObjectInfo.hideObjectType;
            playerCore.nowHideObjectType = nowHideObjectType;

            nowHideVC = hideObjectInfo.hideVC;

            hideObjectInfo.isHideTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SneakingHideObjectInfo hideObjectInfo = other.GetComponent<SneakingHideObjectInfo>();

        if (hideObjectInfo != null)
        {
            canHide = false;
            hideObjectInfo.isHideTarget = false;
        }
    }

    private IEnumerator StartMotion()
    {
        isMotion = true;

        yield return new WaitForSeconds(hideTime);

        isMotion = false;
    }
}
