using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootingPlayerHPControler : MonoBehaviour
{
    [Header("HP")]
    public int playerMaxHitPoint;
    public int playerNowHitPoint;

    [Header("Component")]
    [SerializeField] private SEPlayer sePlayer;
    [SerializeField] private Renderer playerBody;
    [SerializeField] private Renderer playerFace;
    [SerializeField] private Renderer playerGun;

    [Header("Damage")]
    [SerializeField] private float unavailableColliderTime;
    [SerializeField] private int flashNum;

    private ShootingPlayerCore playerCore;
    private Collider playerColl;

    // Start is called before the first frame update
    void Start()
    {
        playerNowHitPoint = playerMaxHitPoint;

        playerCore = GetComponent<ShootingPlayerCore>();
        playerColl = GetComponent<Collider>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerNowHitPoint <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddDamage()
    {
        playerNowHitPoint--;

        StartCoroutine(UnavailableCollider());
    }

    private IEnumerator UnavailableCollider()
    {
        sePlayer.Play("Damage");

        playerColl.enabled = false;

        playerCore.isDamage = true;

        for (int i = 0; i < flashNum; i++)
        {
            playerBody.enabled = false;
            playerFace.enabled = false;
            playerGun.enabled = false;

            yield return new WaitForSeconds(unavailableColliderTime / (flashNum * 2));

            playerBody.enabled = true;
            playerFace.enabled = true;
            playerGun.enabled = true;

            yield return new WaitForSeconds(unavailableColliderTime / (flashNum * 2));
        }

        playerCore.isDamage = false;

        playerColl.enabled = true;
    }
}
