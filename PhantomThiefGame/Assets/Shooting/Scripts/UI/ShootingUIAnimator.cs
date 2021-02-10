using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUIAnimator : MonoBehaviour
{
    [SerializeField] private ShootingPlayerCore playerCore;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsDamage", playerCore.isDamage);
    }
}
