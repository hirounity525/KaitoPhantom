using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseFriendAnimator : MonoBehaviour
{
    [SerializeField] private DefenseFriendCore friendCore;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsDamage",friendCore.isDamage);
    }
}
