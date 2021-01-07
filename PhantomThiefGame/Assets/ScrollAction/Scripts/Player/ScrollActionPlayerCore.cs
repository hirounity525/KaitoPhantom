using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPlayerCore : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private float invincibleTime;
    [SerializeField] private ScrollActionLaserAttacker scrollActionLaserAttacker;
    [SerializeField] private ScrollActionPlayerMover scrollActionPlayerMover;
    //private bool isInvicible;

    // Start is called before the first frame update
    void Start()
    {
       Debug.Log(life);
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollActionLaserAttacker.isLaserHit)
        {
            life -= 1;
            StartCoroutine(Invincible());
            Debug.Log(life);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
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
                life -= 1;
                StartCoroutine(Invincible());
                Debug.Log(life);
                //scrollActionPlayerMover.KnockBack();
            }
        //}
    }

    IEnumerator Invincible()
    {
        //isInvicible = true;
        scrollActionPlayerMover.KnockBack();
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = LayerMask.NameToLayer("Player");
        //isInvicible = false;
    }
}
