using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//フェードアニメーション
public class FadeAnimator : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void FadeIn()
    {
        image.color = Color.white;
        image.DOFade(0, fadeTime);
    }
}
