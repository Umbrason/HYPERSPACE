

using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour, IDamageReciever
{
    public void OnHit(int damage) => DamageNumbers.Spawn(damage, transform.position);
}
