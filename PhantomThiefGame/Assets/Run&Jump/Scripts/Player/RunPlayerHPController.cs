using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerHPController : MonoBehaviour
{
    [SerializeField] private int playerFirstHP;
    [SerializeField] private float unavailableColliderTime;

    private Transform playerTrans;

    [SerializeField] private int playerNowHP;

    private bool unavailable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GetComponent<Transform>();

        playerNowHP = playerFirstHP;
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

        yield return new WaitForSeconds(unavailableColliderTime);

        unavailable = false;
    }
}
