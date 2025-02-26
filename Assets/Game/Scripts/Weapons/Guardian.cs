using UnityEngine;

public class Guardian : OrbitingWeapon
{
    public Transform ProjectileSystem => projectileSystem;
    [SerializeField] private Transform projectileSystem;

    private void Update()
    {
        // Luôn luôn quay vòng tròn
        if (projectileSystem != null)
        {
            projectileSystem.Rotate(Vector3.forward, weaponInfo._speed * Time.deltaTime);
        }
    }

    public void SetUpSaw()
    {
        if (projectileSystem == null) return;
        CircleLayout circleLayout = projectileSystem.GetComponent<CircleLayout>();
        circleLayout.radius = weaponInfo._range;
        circleLayout.ArrangeChildren();
    }

    protected override void Attack()
    {
        foreach (Transform child in projectileSystem)
        {
            child.gameObject.SetActive(true);
        }
    }

    protected override void Disappear()
    {
        foreach (Transform child in projectileSystem)
        {
            child.GetComponent<Projectile_Guardian>().DisappearAnimation();
        }
    }
}
