using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattlePlayerCore : MonoBehaviour
{
    public bool isInvicible;
    public int life;
    [SerializeField] private float invincibleTime;
    //[SerializeField] private ScrollActionLaserAttacker scrollActionLaserAttacker;
    [SerializeField] private BossBattlePlayerMover bossBattlePlayerMover;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(life);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (scrollActionLaserAttacker.isLaserHit)
        {
            life -= 1;
            StartCoroutine(Invincible());
            Debug.Log(life);
        }*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            life = 0;
            Debug.Log(life);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            life--;
            StartCoroutine(Invincible());
            Debug.Log(life);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("トリガー");
        if (!isInvicible)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                life -= 1;
                StartCoroutine(Invincible());
                Debug.Log(life);
                //scrollActionPlayerMover.KnockBack();
            }
        }
    }

    IEnumerator Invincible()
    {
        isInvicible = true;
        bossBattlePlayerMover.KnockBack();
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = LayerMask.NameToLayer("Player");
        isInvicible = false;
    }
}
