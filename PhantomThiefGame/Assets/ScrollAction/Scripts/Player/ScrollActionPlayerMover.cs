using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPlayerMover : MonoBehaviour
{
    [SerializeField] private ScrollActionInputProvider inputProvider;

    [Header("移動")]
    [SerializeField, Tooltip("移動速度")] private float moveSpeed;

    [Header("ジャンプ")]
    [SerializeField, Tooltip("ジャンプ力")] private float jumpPower;
    [SerializeField, Tooltip("最大ジャンプ時間")] private float jumpTime;
    [SerializeField, Tooltip("重力")] private float gravityScale;

    [Header("空中")]
    [SerializeField, Tooltip("方向キー入力時、1フレーム当たりの増減量")] private float airMoveRate;
    [SerializeField, Tooltip("未入力時、1フレーム当たりの減衰量")] private float airMoveLinearDrag;

    [Header("Ray")]
    [SerializeField] private float hitRadius;
    [SerializeField] private float hitDistance;
    [SerializeField] private LayerMask hitLayer;

    [SerializeField] private GameObject phantom_chan;

    [Header("ノックバック")]
    [SerializeField,Tooltip("プレイヤーが右を向いているときのノックバックする力")] private Vector3 knockBackPower2;
    [SerializeField,Tooltip("プレイヤーが左を向いているときのノックバックする力")] private Vector3 knockBackPower3;
    //[SerializeField] private float knockBackPower;
    [SerializeField] private float canNotMoveTime;
    private bool isKockBack = false;

    private Rigidbody rb;
    private Transform playerTrans;

    private bool isGround;
    private bool isMove;
    private bool isJump;

    private bool startsJump;
    private bool endsJump;
    private bool forbidsJump;

    private float nowJumpPower;
    private float jumpTimer;

    private float moveVecTempX;

    private bool startsFall;

    private bool playerDirectionIsRight = true;

    [SerializeField] private ScrollActionPlayerInfo playerInfo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
    }

    void Update()
    {

        if (inputProvider.moveHorizontal == 0)
        {
            isMove = false;
        }

        else
        {
            isMove = true;
        }

        playerInfo.isMove = isMove;
        playerInfo.isKnockBack = isKockBack;

        if (!isKockBack)
        {
            //ジャンプボタン押した時
            if (inputProvider.isJumpButtonDown)
            {
                startsJump = true;
            }

            //ジャンプボタン離した時
            if (inputProvider.isJumpButtonUp)
            {
                endsJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isKockBack)
        {
            //地面についているか
            isGround = IsGround();

            //横方向の移動の入力・正規化
            Vector3 moveVec = Vector3.right * inputProvider.moveHorizontal;
            moveVec = moveVec.normalized;

            //地面についている時かつジャンプできる状態の時
            if (isGround && !forbidsJump)
            {
                //移動速度代入
                moveVec.x *= moveSpeed;

                if (startsJump)
                {
                    //即ジャンプ禁止
                    StartCoroutine(StartJump());

                    //リセット
                    nowJumpPower = jumpPower;
                    jumpTimer = 0;

                    //ジャンプ力代入
                    moveVec.y = nowJumpPower;

                    //現在の移動速度保存
                    moveVecTempX = moveVec.x;

                    isJump = true;
                    startsJump = false;
                    endsJump = false;
                }


            }
            else
            {
                //ジャンプ中に押しても意味がないようにする
                startsJump = false;

                //空中時の移動速度変化
                if (moveVec.x >= 0.1f)
                {
                    moveVecTempX += airMoveRate;
                }
                else if (moveVec.x <= -0.1f)
                {
                    moveVecTempX -= airMoveRate;
                }
                else
                {
                    if (moveVecTempX >= 0.1f)
                    {
                        moveVecTempX -= airMoveLinearDrag;
                    }
                    else if (moveVecTempX <= -0.1f)
                    {
                        moveVecTempX += airMoveLinearDrag;
                    }
                }

                //最大移動速度内に修正・代入
                moveVecTempX = Mathf.Clamp(moveVecTempX, -moveSpeed, moveSpeed);
                moveVec.x = moveVecTempX;

                //落ち始めたら
                if (startsFall)
                {
                    isJump = false;
                    startsFall = false;
                }

                //ジャンプ時
                if (isJump)
                {
                    jumpTimer += Time.fixedDeltaTime;

                    if (jumpTimer < jumpTime)
                    {
                        if (!endsJump)
                        {
                            nowJumpPower = jumpPower - (jumpPower * (jumpTimer / jumpTime));
                            moveVec.y = nowJumpPower;
                        }
                        else
                        {
                            startsFall = true;
                            endsJump = false;
                        }
                    }
                    else
                    {
                        startsFall = true;
                        endsJump = false;
                    }
                }
                else
                {
                    //rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
                    moveVec.y = rb.velocity.y;
                }
            }

            //速度代入
            rb.velocity = moveVec;

            playerInfo.isJump = !isGround;
        }

        if(inputProvider.moveHorizontal > 0)
        {
            playerDirectionIsRight = true;
            phantom_chan.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if(inputProvider.moveHorizontal < 0)
        {
            playerDirectionIsRight = false;
            phantom_chan.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
    }

    private bool IsGround()
    {
        RaycastHit hit;
        bool isGround = Physics.SphereCast(playerTrans.position, hitRadius, Vector3.down, out hit, hitDistance, hitLayer);
        return isGround;
    }

    private IEnumerator StartJump()
    {
        forbidsJump = true;

        yield return new WaitForSeconds(0.5f);

        forbidsJump = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.down * hitDistance, hitRadius);
    }

    public void KnockBack()
    {
        isKockBack = true;
        rb.velocity = Vector3.zero;
        if (playerDirectionIsRight)
        {
            rb.AddForce(knockBackPower2, ForceMode.VelocityChange);
        }
        else if (!playerDirectionIsRight)
        {
            rb.AddForce(knockBackPower3, ForceMode.VelocityChange);
        }
        //rb.AddForce(-transform.right * knockBackPower, ForceMode.VelocityChange);
        StartCoroutine(CanNotMove());
        //Debug.Log(transform.right);
    }

    private IEnumerator CanNotMove()
    {
        yield return new WaitForSeconds(canNotMoveTime);
        isKockBack = false;
    }
}