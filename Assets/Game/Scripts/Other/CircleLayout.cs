using UnityEngine;

[ExecuteAlways]
public class CircleLayout : MonoBehaviour
{
    public float radius = 2f;
    public bool autoUpdate = true;

    private void OnValidate()
    {
        if (autoUpdate)
            ArrangeChildren();
    }

    public void ArrangeChildren()
    {
        int childCount = transform.childCount;
        if (childCount == 0) return;

        float angleStep = 360f / childCount;

        for (int i = 0; i < childCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            Transform child = transform.GetChild(i);
            child.localPosition = pos;
            child.up = pos.normalized; // Hướng ra ngoài
        }
    }
}
