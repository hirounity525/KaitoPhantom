using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingEnemyFlasher : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private GameObject discoverColl;

    [SerializeField] private float lightIntensity;

    [SerializeField] private float flashTime;
    [SerializeField] private int flashNum;
    [SerializeField] private float afterFlashWaitTime;

    [SerializeField] private float lightOnTime;
    [SerializeField] private float lightOffTime;

    private SneakingEnemyCore enemyCore;
    private Transform enemyTrans;

    private Vector3 firstPos;
    private Quaternion firstRot;

    private Coroutine nowPlayCoroutine;

    private void Awake()
    {
        enemyCore = GetComponent<SneakingEnemyCore>();
        enemyTrans = GetComponent<Transform>();

        firstPos = enemyTrans.position;
        firstRot = enemyTrans.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        flashLight.intensity = 0;
        discoverColl.SetActive(false);

        StartCoroutine(FlashLight());
    }

    private void Update()
    {
        if (enemyCore.isDiscovery)
        {
            flashLight.intensity = lightIntensity;
            StopAllCoroutines();
        }

        if (enemyCore.isReset)
        {
            enemyTrans.position = firstPos;
            enemyTrans.rotation = firstRot;

            flashLight.intensity = 0;
            discoverColl.SetActive(false);
            StartCoroutine(FlashLight());

            enemyCore.isReset = false;
        }
    }



    private IEnumerator FlashLight()
    {
        while (true)
        {
            flashLight.DOIntensity(lightIntensity, flashTime).SetLoops(flashNum * 2, LoopType.Yoyo);

            yield return new WaitForSeconds(flashTime * flashNum * 2 + afterFlashWaitTime);

            discoverColl.SetActive(true);
            flashLight.intensity = lightIntensity;

            yield return new WaitForSeconds(lightOnTime);

            discoverColl.SetActive(false);
            flashLight.intensity = 0;

            yield return new WaitForSeconds(lightOffTime);
        }
    }
}
