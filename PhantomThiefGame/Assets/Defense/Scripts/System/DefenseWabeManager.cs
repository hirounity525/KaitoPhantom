using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWabeManager : MonoBehaviour
{
    public DefenseEnemyCreateData[] wabe;
    private DefenseEnemyCreater enemyCreater;

    public int currentWabeNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyCreater = GetComponent<DefenseEnemyCreater>();
        enemyCreater.createData = wabe[currentWabeNum];
    }

    // Update is called once per frame

    private void Update()
    {
    }
}
