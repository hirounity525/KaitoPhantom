using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private ShootingPlayerCore playerCore;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<ShootingPlayerCore>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsDamage", playerCore.isDamage);
    }
}
