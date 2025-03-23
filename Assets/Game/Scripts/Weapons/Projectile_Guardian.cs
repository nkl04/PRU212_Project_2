using UnityEngine;
using DG.Tweening;

public class Projectile_Guardian : Projectile
{
    [SerializeField] private Transform Visual;
    [SerializeField] private float rotateSpeed;

    private void OnEnable()
    {
        Appear();
    }
    protected override void Update()
    {
        Visual.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    public void DisappearAnimation()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f);
    }
}
