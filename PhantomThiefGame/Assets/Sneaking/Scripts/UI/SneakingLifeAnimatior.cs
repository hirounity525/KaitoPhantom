using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SneakingLifeAnimatior : MonoBehaviour
{
    [SerializeField] private Image heartImg;

    public void PlayDeleteAnim(float deleteTime)
    {
        heartImg.DOFillAmount(0, deleteTime);
    }
}
