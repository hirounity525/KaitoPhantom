using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDoorAnimator : MonoBehaviour
{
    [SerializeField] private DefenseEnemyCore enemyCore;

    private Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        doorAnimator.SetBool("IsCreate", enemyCore.isCreate);
        doorAnimator.SetBool("DoorOpen", enemyCore.enemyCreatePos);
        doorAnimator.SetBool("Door1Open", enemyCore.enemyCreatePos1);
        doorAnimator.SetBool("Door2Open", enemyCore.enemyCreatePos2);
        doorAnimator.SetBool("Door3Open", enemyCore.enemyCreatePos3);
        doorAnimator.SetBool("Door4Open", enemyCore.enemyCreatePos4);
        doorAnimator.SetBool("Door5Open", enemyCore.enemyCreatePos5);
    }
}
