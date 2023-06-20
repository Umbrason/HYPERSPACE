using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    private Rigidbody cached_RB;
    private Rigidbody RB => cached_RB ??= GetComponent<Rigidbody>();
    public Vector3 TargetPosition { get; set; }
    [field: SerializeField] public float Speed { get; set; } = 5;

    public void FixedUpdate()
    {
        var delta = TargetPosition - RB.position;
        RB.velocity = delta.normalized * Speed;
        if (Mathf.Abs(RB.position.z - TargetPosition.z) <= Speed * Time.fixedDeltaTime)
        {
            RB.velocity = Vector3.zero;
            RB.position = TargetPosition;
            Destroy(this);
            Destroy(gameObject, .1f + Time.fixedDeltaTime);
        }
    }

    public int Damage = 5;
    void OnTriggerEnter(Collider collider)
    {
        if ((1 << collider.gameObject.layer & LayerMask.GetMask(new[] { "Player" })) != 0) return;
        var parentRecievers = collider.gameObject.GetComponentsInParent<IDamageReciever>();
        var childRecievers = collider.gameObject.GetComponentsInChildren<IDamageReciever>().Where(reciever => reciever.gameObject != collider.gameObject);
        foreach (var reciever in childRecievers.Concat(parentRecievers)) reciever.OnHit(Damage);
        Destroy(this);
        Destroy(gameObject, .1f);
    }
}
