using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWabeManager : MonoBehaviour
{
    public DefenseEnemyCreateData[] wabe;
    private int nowWabeNum;
    private DefenseEnemyCreater enemyCreater;
    // Start is called before the first frame update
    void Start()
    {
        enemyCreater = GetComponent<DefenseEnemyCreater>();
        enemyCreater.createData = wabe[0];
    }

    // Update is called once per frame

}
