using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossBulletCore : MonoBehaviour
{
    [SerializeField] private float bossBulletSpeed;
    [SerializeField] private float DisappearBossBulletTime;
    private float DisappearBossBulletTimeTemp;
    //private BossBattleBossAttacker bossBattleBossAttacker;
    private Rigidbody rb;
    public Vector3 bossBalletVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        DisappearBossBulletTimeTemp += Time.fixedDeltaTime;
        if (DisappearBossBulletTime < DisappearBossBulletTimeTemp)
        {
            DisappearBossBulletTimeTemp = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DisappearBossBulletTimeTemp = 0;
            gameObject.SetActive(false);
        }
    }

    public void MoveBossBullet()
    {
        //bossBalletVec = bossBattleBossAttacker.bossBulletVec2;
        //Debug.Log("move" + bossBalletVec);
        rb.velocity = bossBalletVec * bossBulletSpeed;
    }
}
