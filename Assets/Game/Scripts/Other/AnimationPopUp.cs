using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPopup : MonoBehaviour
{
    public Image background;

    public RectTransform content;

    public PopupAnimationType animationType = PopupAnimationType.moveLeft;

    [SerializeField] Transform[] transformsPartContent;

    List<float> valueScalePartContent = new List<float>();

    public float backgroundEndAlpha = .9f;

    public float showDuration = 0;

    private const float ANIMATION_DURATION = .25f;

    [SerializeField] float timeZoomOut = .3f;

    [SerializeField] float timeDelayZoomOut = .15f;

    private float initialPositionX = 0, initialPositionY = 0;

    private float hidePositionX = 0, hidePositionY = 0;

    private bool isAnimationCompleted = true;

    public System.Action<bool> onCompleted, onAnimationCompleted, onAnimationStart;

    float valueScale;

    [SerializeField] bool destroyOnHide = false;

    [SerializeField] bool checkStatusGameRun = true;

    [SerializeField] bool scalePartOnHide = true;

    private void Awake()
    {
        initialPositionX = transform.localPosition.x;

        initialPositionY = content.localPosition.y;

        hidePositionX = initialPositionX - Screen.width * 2;

        hidePositionY = initialPositionY + Screen.height / 3;

        valueScale = WidthScaleRatio();

        //UnZoomPartContent();
    }
    public float WidthScaleRatio()
    {
        float value = 1;

        float w = Screen.width;
        float h = Screen.height;
        float t = w / h;

        float tD = 1080f / 1920f;

        if (t < tD)
        {
            value = t / tD;
        }

        //print("------>>>" + value);

        return value;
    }

    private void Start()
    {
        //SoundController.instance?.AddSoundForButtons(transform);

        GetValueScalePartContent();

        UnZoomPartContent();
    }

    private void OnEnable()
    {
        CancelInvoke();

        if (showDuration > 0) Invoke(nameof(OnHide), showDuration);

        OnShow();

        Invoke(nameof(ZoomOutPartContent), ANIMATION_DURATION / 3);
    }

    private void OnDisable()
    {
        UnZoomPartContent();
    }

    private void HideContent()
    {
        if (animationType == PopupAnimationType.scale)
        {
            content.localScale = Vector3.zero;
        }
        else if (animationType == PopupAnimationType.moveDown)
        {
            content.localPosition = new Vector2(content.localPosition.x, hidePositionY);
        }
        else
        {
            if (animationType == PopupAnimationType.moveLeft)
            {
                hidePositionX = -Mathf.Abs(hidePositionX);
            }
            else
            {
                hidePositionX = Mathf.Abs(hidePositionX);
            }

            content.localPosition = new Vector2(hidePositionX, content.localPosition.y);
        }

        if (background != null) background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
    }

    public void OnShow()
    {
        Invoke(nameof(SetStopGame), ANIMATION_DURATION);

        onAnimationStart?.Invoke(true);

        if (animationType == PopupAnimationType.none)
        {
            content.gameObject.SetActive(true);

            onCompleted?.Invoke(true);

            onAnimationCompleted?.Invoke(true);

            return;
        }

        if (animationType == PopupAnimationType.scale)
        {
            if (!gameObject.activeInHierarchy) gameObject.SetActive(true);

            ContentScaleAnimation(true);
        }
        else if (animationType == PopupAnimationType.moveDown)
        {
            ContentMoveDownAnimation(true);
        }
        else
        {
            ContentMoveAnimation(true);
        }

        FadeBackground(true);
    }

    public void OnHide()
    {
        if (!isAnimationCompleted) return;

        onAnimationStart?.Invoke(false);

        CancelInvoke();

        ExecuteHidePopup();
    }

    private void ExecuteHidePopup()
    {
        try
        {
            if (animationType == PopupAnimationType.none)
            {
                ContentNoneAnimation();

                return;
            }

            if (animationType == PopupAnimationType.scale)
            {
                ContentScaleAnimation(false);
            }
            else if (animationType == PopupAnimationType.moveDown)
            {
                ContentMoveDownAnimation(false);
            }
            else
            {
                ContentMoveAnimation(false);
            }

            FadeBackground(false);
        }
        catch (System.Exception) { }
    }

    private void FadeBackground(bool isShow)
    {
        if (background == null) return;

        var showAlpha = backgroundEndAlpha;

        var startValue = isShow ? 0 : showAlpha;

        var endValue = isShow ? showAlpha : 0;

        background.color = new Color(background.color.r, background.color.g, background.color.b, startValue);

        background.DOFade(endValue, ANIMATION_DURATION).SetUpdate(true).SetEase(Ease.Linear).onComplete = () => { };
    }

    private void ContentNoneAnimation()
    {
        isAnimationCompleted = true;

        if (scalePartOnHide)
        {
            ZoomInPartContent(() =>
            {
                onCompleted?.Invoke(false);

                onAnimationCompleted?.Invoke(false);

                ContentAnimationCompletion(false);

                ReturnStatusClick(false);
            });
        }
        else
        {
            onCompleted?.Invoke(false);

            onAnimationCompleted?.Invoke(false);

            ContentAnimationCompletion(false);

            ReturnStatusClick(false);
        }
    }

    private void ContentMoveDownAnimation(bool isShow)
    {
        isAnimationCompleted = false;

        var startValue = isShow ? hidePositionY : initialPositionY;

        var endValue = isShow ? initialPositionY : hidePositionY;

        content.localPosition = new Vector2(content.localPosition.x, startValue);

        content.DOLocalMoveY(endValue, ANIMATION_DURATION).SetUpdate(true).SetEase(Ease.Linear).onComplete = () =>
        {
            ReturnStatusClick(isShow);

            ContentAnimationCompletion(isShow);
        };
    }

    private void ContentMoveAnimation(bool isShow)
    {
        isAnimationCompleted = false;

        if (animationType == PopupAnimationType.moveLeft)
        {
            hidePositionX = -Mathf.Abs(hidePositionX);
        }
        else
        {
            hidePositionX = Mathf.Abs(hidePositionX);
        }

        var startValue = isShow ? hidePositionX : initialPositionX;

        var endValue = isShow ? initialPositionX : hidePositionX;

        content.localPosition = new Vector2(startValue, content.localPosition.y);

        content.DOAnchorPosX(endValue, ANIMATION_DURATION).SetUpdate(true).SetEase(Ease.Linear).onComplete = () =>
        {
            ReturnStatusClick(isShow);

            ContentAnimationCompletion(isShow);
        };
    }

    private void ContentScaleAnimation(bool isShow)
    {
        isAnimationCompleted = false;

        var startValue = isShow ? 0 : 1 * valueScale;

        var endValue = isShow ? 1 * valueScale : 0;

        content.localScale = new Vector3(startValue, startValue, startValue);

        content.DOScale(new Vector3(endValue, endValue, endValue), ANIMATION_DURATION).SetUpdate(true).SetEase(isShow ? Ease.OutQuad : Ease.InSine).onComplete = () =>
        {
            ReturnStatusClick(isShow);

            ContentAnimationCompletion(isShow);
        };
    }

    void SetStopGame()
    {
        //if (checkStatusGameRun)
        //{
        //    this.PostEvent(EventID.StatusGameRun, false);
        //}
    }

    void ReturnStatusClick(bool _isShow)
    {
        //if (!_isShow)
        //{
        //    if (checkStatusGameRun)
        //    {
        //        this.PostEvent(EventID.StatusGameRun, true);
        //    }
        //}
    }

    private void ContentAnimationCompletion(bool isShow)
    {
        try
        {
            if (!isShow)
            {
                if (destroyOnHide)
                {
                    onCompleted?.Invoke(isShow);

                    Destroy(gameObject);

                    return;
                }

                gameObject.SetActive(false);
            }

            isAnimationCompleted = true;

            onCompleted?.Invoke(isShow);

            if (isShow)
            {
                Invoke(nameof(CallCallbackAnimationComplete), .5f);
            }
            else
            {
                onAnimationCompleted?.Invoke(false);
            }
        }
        catch (System.Exception) { }
    }

    private void CallCallbackAnimationComplete()
    {
        onAnimationCompleted?.Invoke(true);
    }

    void UnZoomPartContent()
    {
        foreach (var part in transformsPartContent)
        {
            part.localScale = Vector2.zero;
        }
    }


    void GetValueScalePartContent()
    {
        valueScalePartContent.Clear();

        for (int i = 0; i < transformsPartContent.Length; i++)
        {
            valueScalePartContent.Add(transformsPartContent[i].localScale.x);
        }
    }

    void ZoomOutPartContent()
    {
        StartCoroutine(WaitZoomOutContent());
    }

    void ZoomInPartContent(System.Action callback)
    {
        StartCoroutine(WaitZoomInContent(callback));
    }

    IEnumerator WaitZoomInContent(System.Action callback = null)
    {
        for (int i = transformsPartContent.Length - 1; i >= 0; i--)
        {
            if (!transformsPartContent[i].gameObject.activeInHierarchy) continue;

            transformsPartContent[i].DOScale(Vector2.zero, timeZoomOut).SetEase(Ease.InQuad);
            yield return new WaitForSeconds(timeDelayZoomOut);
        }

        callback?.Invoke();
    }


    IEnumerator WaitZoomOutContent(System.Action callback = null)
    {
        for (int i = 0; i < transformsPartContent.Length; i++)
        {
            if (transformsPartContent[i].gameObject.activeInHierarchy) ZoomOutPart(transformsPartContent[i], valueScalePartContent[i]);
            else continue;

            yield return new WaitForSeconds(timeDelayZoomOut);
        }

        if (callback != null) callback.Invoke();
    }

    void ZoomOutPart(Transform part, float valueScale)
    {
        part.DOScale(Vector2.one * valueScale, timeZoomOut).SetEase(Ease.OutBack);
    }
}

public enum PopupAnimationType
{
    scale,
    moveLeft,
    moveRight,
    moveDown,
    none
}
