
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    public static Dictionary<Guid, BoidMovement> BoidMembers = new();
    public Vector3 InputTargetPosition { get; set; }
    public const float boidForceMultiplier = 200f;
    public float Speed = 5f;

    private ITargetPositionModifier[] cached_targetPositionModifiers;
    private ITargetPositionModifier[] TargetPositionModifiers => cached_targetPositionModifiers ??= GetComponents<ITargetPositionModifier>();

    private Guid boidID = Guid.NewGuid();
    void OnEnable() => BoidMembers.Add(boidID, this);
    void OnDisable() => BoidMembers.Remove(boidID);

    Rigidbody RB => GetComponent<Rigidbody>();
    void FixedUpdate()
    {

        var targetPosition = InputTargetPosition;
        foreach (var modifier in TargetPositionModifiers)
            targetPosition = modifier.Modify(targetPosition);

        var velocity = Vector2.zero;
        foreach (var boidMember in BoidMembers.Values.Where(boidMember => boidMember != this))
        {
            var delta = (Vector2)(transform.position - boidMember.transform.position);
            velocity += Mathf.Pow(1f / delta.magnitude, 5f) * delta * boidForceMultiplier * Speed;
        }
        velocity += Vector2.ClampMagnitude(targetPosition - transform.position, 1f) * Speed;
        velocity = Vector2.ClampMagnitude(velocity, Speed);
        RB.velocity = velocity;
    }
}
