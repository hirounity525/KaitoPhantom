using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerHPControler : MonoBehaviour
{
    [SerializeField] public int playerMaxHitPoint;
    [SerializeField] private float unavailableColliderTime;
    public int playerNowHitPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerNowHitPoint=playerMaxHitPoint;
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

        yield return new WaitForSeconds(unavailableColliderTime);

        this.GetComponent<Collider>().enabled = true;
    }
}
