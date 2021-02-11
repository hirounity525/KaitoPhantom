using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPlayerCore : MonoBehaviour
{
    public bool isGameOver;
    public int life;
    public bool isInvicible;
    [SerializeField] private float invincibleTime;
    [SerializeField] private ScrollActionLaserAttacker scrollActionLaserAttacker;
    [SerializeField] private ScrollActionLaserAttacker scrollActionLaserAttacker2;
    [SerializeField] private ScrollActionLaserAttacker scrollActionLaserAttacker3;
    [SerializeField] private ScrollActionPlayerMover scrollActionPlayerMover;
    [SerializeField] private SEPlayer sEPlayer;
    [SerializeField] private SEPlayer sEPlayer2;

    // Start is called before the first frame update
    void Start()
    {
       Debug.Log(life);
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollActionLaserAttacker.isLaserHit  ||
            scrollActionLaserAttacker2.isLaserHit ||
            scrollActionLaserAttacker3.isLaserHit)
        {
            sEPlayer2.Play("Damage");
            life -= 1;
            StartCoroutine(Invincible());
            Debug.Log(life);
        }

        if(life <= 0)
        {
            isGameOver = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            sEPlayer.Play("Hole");
            isInvicible = true;
            life = 0;
            Debug.Log(life);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (!isInvicible)
        //{
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
            sEPlayer2.Play("Damage");
                life -= 1;
                StartCoroutine(Invincible());
                Debug.Log(life);
                //scrollActionPlayerMover.KnockBack();
            }
        //}
    }

    IEnumerator Invincible()
    {
        isInvicible = true;
        scrollActionPlayerMover.KnockBack();
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = LayerMask.NameToLayer("Player");
        isInvicible = false;
    }
}
