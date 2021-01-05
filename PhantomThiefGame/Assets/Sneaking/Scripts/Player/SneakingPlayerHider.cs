using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingPlayerHider : MonoBehaviour
{
    [SerializeField] private SneakingInputProvider inputProvider;

    [SerializeField] private float hideTime;

    [SerializeField] private GameObject defaultVC;
    [SerializeField] private GameObject hideVC;

    private SneakingPlayerCore playerCore;
    private Transform playerTrans;
    private Collider coll;
    private Rigidbody rb;

    private bool isHide;
    private bool canHide;

    private Vector3 beforeHidePoint;
    private Vector3 hidePoint;

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

                    defaultVC.SetActive(false);
                    hideVC.SetActive(true);

                    isHide = true;
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

                defaultVC.SetActive(true);
                hideVC.SetActive(false);

                isHide = false;
            }
        }

        playerCore.isHide = isHide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ToTarget")
        {
            canHide = true;
            hidePoint = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ToTarget")
        {
            canHide = false;
        }
    }
}
