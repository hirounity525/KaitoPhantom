using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefenseEnemyType
{
    STRAIGHTER,

}

[System.Serializable]
public struct DefenseEnemyData
{
    [Range(0,5)]public int createPos;
    public DefenseEnemyType enemyType;
    public float createTime;
    public float moveSpeed;
}

[CreateAssetMenu(menuName ="Defense/EnemyCreateData")]
public class DefenseEnemyCreateData : ScriptableObject
{
    public DefenseEnemyData[] enemyDatas;
}
