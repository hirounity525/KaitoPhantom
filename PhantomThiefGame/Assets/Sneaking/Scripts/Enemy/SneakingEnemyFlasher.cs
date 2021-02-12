using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingEnemyFlasher : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private GameObject discoverColl;

    [SerializeField] private float lightIntensity;
    [SerializeField] private float firstWaitTime;

    [SerializeField] private float flashTime;
    [SerializeField] private int flashNum;
    [SerializeField] private float afterFlashWaitTime;

    [SerializeField] private float lightOnTime;
    [SerializeField] private float lightOffTime;

    private SneakingEnemyCore enemyCore;
    private Transform enemyTrans;
    private SneakingEnemyDiscover enemyDiscover;

    private Vector3 firstPos;
    private Quaternion firstRot;

    private Coroutine nowPlayCoroutine;

    private void Awake()
    {
        enemyCore = GetComponent<SneakingEnemyCore>();
        enemyTrans = GetComponent<Transform>();
        enemyDiscover = GetComponent<SneakingEnemyDiscover>();

        firstPos = enemyTrans.position;
        firstRot = enemyTrans.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        flashLight.intensity = 0;
        enemyDiscover.isLightOn = false;

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
            enemyDiscover.isLightOn = false;
            StartCoroutine(FlashLight());

            enemyCore.isReset = false;
        }
    }



    private IEnumerator FlashLight()
    {
        yield return new WaitForSeconds(firstWaitTime);

        while (true)
        {
            flashLight.DOIntensity(lightIntensity, flashTime).SetLoops(flashNum * 2, LoopType.Yoyo);

            yield return new WaitForSeconds(flashTime * flashNum * 2 + afterFlashWaitTime);

            enemyDiscover.isLightOn = true;
            flashLight.intensity = lightIntensity;

            yield return new WaitForSeconds(lightOnTime);

            enemyDiscover.isLightOn = false;
            flashLight.intensity = 0;

            yield return new WaitForSeconds(lightOffTime);
        }
    }
}
