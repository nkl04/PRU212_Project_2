using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform skullTransform;
    public void FadeOut(Action onCompleteAction = null)
    {
        skullTransform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(1f)
           .Append(skullTransform.DOScale(5f, 0.6f).SetEase(Ease.OutCubic))
           .Append(skullTransform.DOScale(70f, 0.25f).SetEase(Ease.InCubic))
           .OnComplete(() =>
           {
               onCompleteAction();
               gameObject.SetActive(false);
           });
    }

    public void FadeIn(Action onCompleteAction = null)
    {
        skullTransform.localScale = new Vector3(70f, 70f, 70f);
        skullTransform.DOScale(0f, 0.6f).SetEase(Ease.InCubic).OnComplete(() => onCompleteAction());
    }
}
