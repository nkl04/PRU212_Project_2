using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private float dayLength = 120f; // Thời gian cho 1 chu kỳ ngày đêm
    [SerializeField] private Light2D globalLight;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float dayIntensity = 1f;
    [SerializeField] private float nightIntensity = 0.1f;

    private float transparenceNight = 0.9f; // Alpha cao hơn vào ban đêm
    private float transparenceDay = 0f;     // Alpha trong suốt vào ban ngày

    private float currentTime = 0f;

    private void Start()
    {
        globalLight.intensity = dayIntensity;
        background.color = new Color(background.color.r, background.color.g, background.color.b, transparenceDay);
    }

    private void Update()
    {
        // Tính thời gian hiện tại trong chu kỳ ngày/đêm
        currentTime += Time.deltaTime;
        float cycleProgress = (currentTime % dayLength) / dayLength; // Normalized 0 → 1

        // Tính góc quay của mặt trời
        float angle = cycleProgress * 360f;
        globalLight.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Xác định tỉ lệ sáng tối (0: sáng nhất, 1: tối nhất)
        float lightFactor = Mathf.Sin(cycleProgress * Mathf.PI);

        // Cập nhật độ sáng của ánh sáng tổng quát
        globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lightFactor);

        // Cập nhật độ trong suốt của background (trong suốt ban ngày, mờ dần về đêm)
        float alpha = Mathf.Lerp(transparenceNight, transparenceDay, lightFactor);
        background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
    }
}
