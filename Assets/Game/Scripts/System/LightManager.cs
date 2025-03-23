using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Slider daynightSlider;
    [Space(10)]
    [SerializeField] private float dayLength = 120f;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float dayIntensity = 1f;
    [SerializeField] private float nightIntensity = 0.1f;

    private float transparenceNight = 0.9f;
    private float transparenceDay = 0f;
    private float currentTime = 0f;

    private Animator handleRectAnimator;

    private void Start()
    {
        globalLight.intensity = dayIntensity;
        background.color = new Color(background.color.r, background.color.g, background.color.b, transparenceDay);
        daynightSlider.maxValue = 1;
        handleRectAnimator = daynightSlider.handleRect.GetComponent<Animator>();
        daynightSlider.value = 0;
    }

    private bool isIncreasing = true;

    private void Update()
    {
        currentTime += Time.deltaTime;
        float cycleProgress = (currentTime % dayLength) / dayLength;

        float angle = cycleProgress * 360f;
        globalLight.transform.rotation = Quaternion.Euler(0, 0, angle);

        float lightFactor = Mathf.Sin(cycleProgress * Mathf.PI);
        globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lightFactor);
        float alpha = Mathf.Lerp(transparenceNight, transparenceDay, lightFactor);
        background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);

        daynightSlider.value = lightFactor;

        if (daynightSlider.value <= 0.5f)
        {
            handleRectAnimator.Play("Night_Idle");
        }
        else
        {
            handleRectAnimator.Play("Sun_Idle");
        }
    }

}
