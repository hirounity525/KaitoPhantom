using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootingEnemyWeaponType
{
    LASER,
    MISSILE,
    BULLET
}

public class ShootingEnemyWeaponCore : MonoBehaviour
{
    public ShootingEnemyWeaponType weaponType;
}
