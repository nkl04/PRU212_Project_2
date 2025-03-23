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

        seq.SetUpdate(true)
           .AppendInterval(1f)
           .Append(skullTransform.DOScale(3f, 0.6f).SetEase(Ease.OutCubic))
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
        Sequence seq = DOTween.Sequence();

        seq.SetUpdate(true)
           .Append(skullTransform.DOScale(0f, 1f).SetEase(Ease.OutExpo)) // Tăng thời gian lên 1s, dùng Ease mượt hơn
           .AppendInterval(0.3f) // Thêm delay 0.3s trước khi gọi onCompleteAction
           .OnComplete(() => onCompleteAction?.Invoke());

    }
}
