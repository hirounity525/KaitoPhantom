using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingPlayerMover : MonoBehaviour
{
    [SerializeField] private SneakingInputProvider inputProvider;
    [SerializeField] private float moveSpeed;

    private SneakingPlayerCore playerCore;
    private Rigidbody rb;

    private void Awake()
    {
        playerCore = GetComponent<SneakingPlayerCore>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!playerCore.isHide)
        {
            if(playerCore.moveVec.magnitude >= 0.1f)
            {
                playerCore.isMove = true;
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                playerCore.isMove = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!playerCore.isHide)
        {
            Vector3 moveVec = (Vector3.right * inputProvider.moveVec.x + Vector3.forward * inputProvider.moveVec.y);
            rb.velocity = moveVec * moveSpeed + Vector3.up * rb.velocity.y;
            playerCore.moveVec = moveVec;
        }
    }
}
