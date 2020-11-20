using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyCreater : MonoBehaviour
{
    public DefenseEnemyCreateData createData;
    [SerializeField] private GameObject friendObj;
   [SerializeField] private Transform[] createTrans;
   [SerializeField]  private ObjectPool straighterPool;
    private float createTimer;
    private DefenseEnemyData currentEnemyData;
    private int currentDataNum;
    private GameObject createEnemyObj;
    private bool getsEnemyData=true;
    public bool stopCreate=false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (friendObj.activeSelf)
        {
            if (!stopCreate)
            {
                if (getsEnemyData)
                {
                    currentEnemyData = createData.enemyDatas[currentDataNum];
                    getsEnemyData = false;
                }

                if (createTimer >= currentEnemyData.createTime)
                {
                    switch (currentEnemyData.enemyType)
                    {
                        case DefenseEnemyType.STRAIGHTER:
                            createEnemyObj = straighterPool.GetObject();
                            break;
                    }

                    createEnemyObj.GetComponent<DefenseEnemyMover>().enemySpeed = currentEnemyData.moveSpeed;
                    createEnemyObj.transform.position = createTrans[currentEnemyData.createPos].position;
                    if (currentEnemyData.createPos % 2 == 1)
                    {
                        createEnemyObj.transform.rotation = Quaternion.Euler(0, 180, 0);

                    }
                    else
                    {
                        createEnemyObj.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    currentDataNum++;
                    if (currentDataNum >= createData.enemyDatas.Length)
                    {
                        stopCreate = true;
                        createTimer = 0;
                        currentDataNum = 0;
                        getsEnemyData = true;
                        stopCreate = false;
                    }
                    else
                    {
                        getsEnemyData = true;
                    }

                }
                createTimer += Time.deltaTime;
            }
        }

    }
       



    
}
