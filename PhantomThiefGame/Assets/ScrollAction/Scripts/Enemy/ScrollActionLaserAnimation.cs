using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionLaserAnimation : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] private float laserIdleToLaserDisappearTime;
    [SerializeField] private float laserDisappearToLaserBlinkingTime;
    [SerializeField] private float laserBlinkingToLaserIdleTime;
    private SEPlayer sEPlayer;
    private MeshRenderer meshRenderer;
    private Animator laserAnimator;

    private void Awake()
    {
        sEPlayer = GetComponent<SEPlayer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        laserAnimator = GetComponent<Animator>();
        StartCoroutine(LaserIdleToLaserDisappear());
    }

    // Update is called once per frame
    void Update()
    {
        /*StartCoroutine(LaserIdleToLaserDisappear());
        Debug.Log("1");
        StartCoroutine(LaserDisappearToLaserBlinking());
        Debug.Log("2");
        StartCoroutine(LaserBlinkingToLaserIdle());
        Debug.Log("3");
        */

        if (meshRenderer.enabled)
        {
            sEPlayer.Play("Laser");
        }
    }

    IEnumerator LaserIdleToLaserDisappear()
    {
        laser.SetActive(true);
        yield return new WaitForSeconds(laserIdleToLaserDisappearTime);
        laserAnimator.SetBool("isDisappear", true);
        laserAnimator.SetBool("isBlinking", false);
        //laser.SetActive(false);
        StartCoroutine(LaserDisappearToLaserBlinking());
    }

    IEnumerator LaserDisappearToLaserBlinking()
    {
        laser.SetActive(false);
        yield return new WaitForSeconds(laserDisappearToLaserBlinkingTime);
        laserAnimator.SetBool("isDisappear", false);
        laserAnimator.SetBool("isBlinking", true);
        //laser.SetActive(false);
        StartCoroutine(LaserBlinkingToLaserIdle());
    }

    IEnumerator LaserBlinkingToLaserIdle()
    {
        yield return new WaitForSeconds(laserBlinkingToLaserIdleTime);
        laserAnimator.SetBool("isDisappear", false);
        laserAnimator.SetBool("isBlinking", false);
        //laser.SetActive(true);
        StartCoroutine(LaserIdleToLaserDisappear());
    }
}
