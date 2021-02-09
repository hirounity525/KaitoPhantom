using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerHPControler : MonoBehaviour
{
    [SerializeField] public int playerMaxHitPoint;
    [SerializeField] private float unavailableColliderTime;

    public int playerNowHitPoint;

    private ShootingPlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerNowHitPoint=playerMaxHitPoint;

        playerCore = GetComponent<ShootingPlayerCore>();
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
        this.GetComponent<Collider>().enabled = false;

        playerCore.isDamage = true;

        yield return new WaitForSeconds(unavailableColliderTime);

        playerCore.isDamage = false;

        this.GetComponent<Collider>().enabled = true;
    }
}
