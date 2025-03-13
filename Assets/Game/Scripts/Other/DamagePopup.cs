using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    private void OnEnable()
    {
        transform.DOMove(transform.position + new Vector3(0, 1, 0), 1f).OnComplete(() => gameObject.SetActive(false));
    }
    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

}
