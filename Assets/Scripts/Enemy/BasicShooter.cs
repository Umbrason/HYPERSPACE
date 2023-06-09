
using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    float ChargeTime = .5f;
    [SerializeField] private Projectile ProjectileTemplate;

    private float lastShot = -1f;
    void FixedUpdate()
    {
        if (Time.time < lastShot + ChargeTime) return;
        lastShot = Time.time;
        Shoot();
    }

    private void Shoot()
    {
        var projectileInstance = Instantiate(ProjectileTemplate);
        projectileInstance.transform.position = transform.position + Vector3.back * 2f;
        projectileInstance.TargetPosition = (Vector2)transform.position;
    }
}

