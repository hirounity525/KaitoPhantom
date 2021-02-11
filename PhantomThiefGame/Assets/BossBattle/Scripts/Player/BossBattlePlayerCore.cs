using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattlePlayerCore : MonoBehaviour
{
    public bool isGameOver;
    public bool isInvicible;
    public int life;
    [SerializeField] private float invincibleTime;
    //[SerializeField] private ScrollActionLaserAttacker scrollActionLaserAttacker;
    [SerializeField] private BossBattlePlayerMover bossBattlePlayerMover;
    [SerializeField] private SEPlayer sEPlayer4;
    [SerializeField] private BossBattleBossCore bossCore;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(life);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossCore.isClear || !bossCore.isClearTemp)
        {
            if (life <= 0)
            {
                isGameOver = true;
                Debug.Log("GameOver");
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            sEPlayer4.Play("Damage");
            life = 0;
            Debug.Log(life);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            sEPlayer4.Play("Damage");
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
                sEPlayer4.Play("Damage");
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
