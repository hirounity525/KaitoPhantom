using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossAttacker : MonoBehaviour
{
    [SerializeField] private GameObject guardPrefab;
    [SerializeField] private float summonGuardsXposMax;
    [SerializeField] private float summonGuardsXposMin;
    [SerializeField] private float summonGuardsYpos;

    [SerializeField] private GameObject rockPrefabs;
    [SerializeField] private float summonRockXposMax;
    [SerializeField] private float summonRockXposMin;
    [SerializeField] private float summonRockYpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SummonGuards()
    {
        Instantiate(guardPrefab, new Vector3(Random.Range(summonGuardsXposMin,summonGuardsXposMax), summonGuardsYpos, 0), Quaternion.identity);
    }

    void SummonRock()
    {
        Instantiate(rockPrefabs, new Vector3(Random.Range(summonRockXposMin, summonRockXposMax), summonRockYpos, 0), Quaternion.identity);
    }

    void GunAttack()
    {

    }
}
