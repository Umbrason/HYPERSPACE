
using UnityEngine;

public interface IDamageReciever
{
    public void OnHit(int damage);
    public GameObject gameObject { get; }
}
