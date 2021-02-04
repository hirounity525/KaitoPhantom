using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleRocksCore : MonoBehaviour
{
    [SerializeField] private float DisappearRockTime;
    private float DisappearRockTimeTemp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void FixedUpdate()
    {
        DisappearRockTimeTemp += Time.fixedDeltaTime;
        if(DisappearRockTime < DisappearRockTimeTemp)
        {
            DisappearRockTimeTemp = 0;
            gameObject.SetActive(false);
        }
    }
}
