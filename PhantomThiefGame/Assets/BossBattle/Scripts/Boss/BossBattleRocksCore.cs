using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleRocksCore : MonoBehaviour
{
    [SerializeField] private float DisappearRockTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisappearRock());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisappearRock()
    {
        yield return new WaitForSeconds(DisappearRockTime);
        gameObject.SetActive(false);
    }
}
