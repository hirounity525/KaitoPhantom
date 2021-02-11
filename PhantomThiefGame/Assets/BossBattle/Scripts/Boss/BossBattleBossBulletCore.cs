using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossBulletCore : MonoBehaviour
{
    public Transform playerTrans;
    [SerializeField] private float bossBulletSpeed;
    [SerializeField] private float DisappearBossBulletTime;
    private float DisappearBossBulletTimeTemp;
    //private BossBattleBossAttacker bossBattleBossAttacker;
    private Rigidbody rb;
    public Vector3 bossBalletVec;

    private bool isOne = true;

    private SEPlayer sEPlayer;
    private bool isOne2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sEPlayer = GetComponent<SEPlayer>();
    } 

    // Update is called once per frame
    void Update()
    {
        if (isOne)
        {
            this.transform.LookAt(playerTrans);
            isOne = false;
        }

        if (!isOne2)
        {
            sEPlayer.Play("GunAttack");
            isOne2 = true;
        }
    }

    private void FixedUpdate()
    {
        DisappearBossBulletTimeTemp += Time.fixedDeltaTime;
        if (DisappearBossBulletTime < DisappearBossBulletTimeTemp)
        {
            DisappearBossBulletTimeTemp = 0;
            gameObject.SetActive(false);
            isOne2 = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DisappearBossBulletTimeTemp = 0;
            gameObject.SetActive(false);
            isOne2 = false;
        }
    }

    public void MoveBossBullet()
    {
        //bossBalletVec = bossBattleBossAttacker.bossBulletVec2;
        //Debug.Log("move" + bossBalletVec);
        rb.velocity = bossBalletVec * bossBulletSpeed;
    }

    private void OnEnable()
    {
        isOne = true;
    }
}
