using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossBulletCore : MonoBehaviour
{
    [SerializeField] private float DisappearBossBulletTime;
    [SerializeField] private float bossBulletSpeed;
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
        //rb = GetComponent<Rigidbody>();
        StartCoroutine(DisappearBossBullet());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DisappearBossBullet()
    {
        yield return new WaitForSeconds(DisappearBossBulletTime);
        gameObject.SetActive(false);
    }

    public void MoveBossBullet()
    {
        //bossBalletVec = bossBattleBossAttacker.bossBulletVec2;
        //Debug.Log("move" + bossBalletVec);
        rb.velocity = bossBalletVec * bossBulletSpeed;
    }
}
