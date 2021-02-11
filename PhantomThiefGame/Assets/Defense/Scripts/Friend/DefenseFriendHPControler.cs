using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseFriendHPControler : MonoBehaviour
{
    [SerializeField] private SEPlayer sePlayer;
    [SerializeField] private float damageAnimationTime;

    public int maxHitpoints;
    public int hitPoints;

    private DefenseFriendCore friendCore;

    // Start is called before the first frame update
    private void OnEnable()
    {
        hitPoints = maxHitpoints;
    }

    private void Start()
    {
        friendCore = GetComponent<DefenseFriendCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddDamage()
    {
        if(hitPoints > 0)
        {
            hitPoints--;
            StartCoroutine(DamageAnimation());
        }
    }

    private IEnumerator DamageAnimation()
    {
        sePlayer.Play("Damage");

        friendCore.isDamage = true;

        yield return new WaitForSeconds(damageAnimationTime);

        friendCore.isDamage = false;
    }

}
