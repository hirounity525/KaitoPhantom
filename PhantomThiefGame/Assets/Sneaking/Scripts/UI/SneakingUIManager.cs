using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SneakingUIManager : MonoBehaviour
{
    public bool isDeleteLife;

    [SerializeField] private SneakingLifeController lifeController;
    [SerializeField] private Image blackPanel;
    [SerializeField] private float deleteWaitTime;

    public void StartLifeDelete(float waitTime)
    {
        StartCoroutine(DeleteLifeCoroutine(waitTime));
    }

    private IEnumerator DeleteLifeCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        blackPanel.color = Color.black;

        yield return new WaitForSeconds(deleteWaitTime);

        lifeController.DeleteLife();

        yield return new WaitForSeconds(lifeController.LifeDeleteTime() + deleteWaitTime);

        isDeleteLife = true;
    }

    public void FadeOutPanel(float fadeTime)
    {
        blackPanel.DOFade(0, fadeTime).SetEase(Ease.InExpo);
    }
}
