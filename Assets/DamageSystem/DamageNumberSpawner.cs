

using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour, IDamageReciever
{
    int damageThisFrame = 0;
    public void OnHit(int damage) => damageThisFrame += damage;

    void FixedUpdate()
    {
        if (damageThisFrame == 0) return;
        DamageNumbers.Spawn(damageThisFrame, transform.position);
        damageThisFrame = 0;
    }
}
