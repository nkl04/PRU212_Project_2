using UnityEngine;

public class Guardian : OrbitingWeapon
{
    public Transform ProjectileSystem => projectileSystem;
    [SerializeField] private Transform projectileSystem;
    public float RotateSpeed { get; set; }

    private void Awake()
    {
        RotateSpeed = weaponInfo._speed;
    }
    private void Update()
    {
        // Lu�n lu�n quay v�ng tr�n
        if (projectileSystem != null)
        {
            projectileSystem.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime);
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
            child.GetComponent<Projectile_Guardian>().SetDamage(ATKMultiplier * Player.PlayerStats.Damage);
            Debug.Log(ATKMultiplier * Player.PlayerStats.Damage);
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
