using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyCreater : MonoBehaviour
{
    public DefenseEnemyCreateData createData;
    [SerializeField] private GameObject friendObj;
    [SerializeField] private Transform[] createTrans;
    [SerializeField] private ObjectPool straighterPool;
    private float createTimer;
    private DefenseEnemyData currentEnemyData;
    private int currentDataNum;
    private GameObject createEnemyObj;
    private bool getsEnemyData = true;
    public bool stopCreate = false;
    private DefenseWabeManager wabeManager;
    private DefenseEnemyCore enemyCore;
    [SerializeField] private float waitAnimation;
    private bool gameStop = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyCore = GetComponent<DefenseEnemyCore>();
        wabeManager = GetComponent<DefenseWabeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wabeManager.currentWabeNum == 4)
        {
            gameStop = true;

            //クリア
        }


        if (!gameStop)
        {
            if (friendObj.activeSelf)
            {

                if (currentDataNum >= createData.enemyDatas.Length)
                {
                    wabeManager.currentWabeNum++;
                    createData = wabeManager.wabe[wabeManager.currentWabeNum];
                    stopCreate = true;
                    createTimer = 0;
                    getsEnemyData = true;
                    currentDataNum = 0;
                    stopCreate = false;
                }
                else
                {
                    getsEnemyData = true;
                }


                switch (currentEnemyData.createPos)
                {
                    case 0:
                        enemyCore.enemyCreatePos = true;
                        break;
                    case 1:
                        enemyCore.enemyCreatePos1 = true;
                        break;
                    case 2:
                        enemyCore.enemyCreatePos2 = true;
                        break;
                    case 3:
                        enemyCore.enemyCreatePos3 = true;
                        break;
                    case 4:
                        enemyCore.enemyCreatePos4 = true;
                        break;
                    case 5:
                        enemyCore.enemyCreatePos5 = true;
                        break;
                }


                if (!stopCreate)
                {
                    if (getsEnemyData)
                    {
                        currentEnemyData = createData.enemyDatas[currentDataNum];
                        getsEnemyData = false;
                    }

                    if (createTimer >= currentEnemyData.createTime)
                    {
                        enemyCore.isCreate = true;

                        switch (currentEnemyData.enemyType)
                        {
                            case DefenseEnemyType.STRAIGHTER:
                                createEnemyObj = straighterPool.GetObject();
                                break;
                        }

                        createEnemyObj.transform.rotation = Quaternion.Euler(0, 35, 0);



                        createEnemyObj.GetComponent<DefenseEnemyMover>().enemySpeed = currentEnemyData.moveSpeed;

                        createEnemyObj.transform.position = createTrans[currentEnemyData.createPos].position;


                        enemyCore.enemyCreatePos = false;
                        enemyCore.enemyCreatePos1 = false;
                        enemyCore.enemyCreatePos2 = false;
                        enemyCore.enemyCreatePos3 = false;
                        enemyCore.enemyCreatePos4 = false;
                        enemyCore.enemyCreatePos5 = false;
                        currentDataNum++;

                    }

                    createTimer += Time.deltaTime;
                }


            }

        }


    }

    private IEnumerator IsCreate()
    {
        enemyCore.isCreate = true;

        yield return new WaitForSeconds(waitAnimation);

        enemyCore.isCreate = false;

    }

}
