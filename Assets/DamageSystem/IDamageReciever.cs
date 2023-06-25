
using UnityEngine;

public interface IDamageReciever
{
    public void OnHit(int damage);
    public bool enabled { get; set; }
    public GameObject gameObject { get; }
}
