using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerHPController : MonoBehaviour
{
    [SerializeField] public int playerMaxHP;
    [SerializeField] private float unavailableColliderTime;

    private RunPlayerCore playerCore;
    private Transform playerTrans;

    public int playerNowHP;

    private bool unavailable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<RunPlayerCore>();
        playerTrans = GetComponent<Transform>();

        playerNowHP = playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNowHP <= 0)
        {
            gameObject.SetActive(false);
        }

        if (playerTrans.position.y <= -8)
        {
            playerNowHP = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject Obstacle = other.gameObject;

        if (!unavailable)
        {
            if (Obstacle.tag == "Enemy")
            {
                playerNowHP--;
                StartCoroutine(UnavailableCollider());
            }
        }

    }

    private IEnumerator UnavailableCollider()
    {
        unavailable = true;
        playerCore.isDamage = true;

        yield return new WaitForSeconds(unavailableColliderTime);

        unavailable = false;
        playerCore.isDamage = false;
    }
}
