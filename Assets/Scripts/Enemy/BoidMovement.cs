
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoidMovement : MonoBehaviour
{
    public static Dictionary<int, Dictionary<Guid, BoidMovement>> BoidMembers = new();
    public Vector3 InputTargetPosition { get; set; }

    private Dictionary<Guid, BoidMovement> BoidGroup => BoidMembers.ContainsKey(m_GroupIndex) ? BoidMembers[m_GroupIndex] : BoidMembers[m_GroupIndex] = new();
    private int m_GroupIndex;
    public int GroupIndex
    {
        get => m_GroupIndex;
        set
        {
            if (m_GroupIndex == value) return;
            BoidGroup.Remove(boidID);
            if (BoidMembers[m_GroupIndex].Count == 0) BoidMembers.Remove(m_GroupIndex);
            m_GroupIndex = value;
            BoidGroup.Add(boidID, this);
        }
    }

    public const float boidForceMultiplier = 200f;
    public float Speed = 5f;

    private ITargetPositionModifier[] cached_targetPositionModifiers;
    private ITargetPositionModifier[] TargetPositionModifiers => cached_targetPositionModifiers ??= GetComponents<ITargetPositionModifier>();

    private Guid boidID = Guid.NewGuid();
    void OnEnable() => BoidGroup.Add(boidID, this);
    void OnDisable() => BoidGroup.Remove(boidID);

    Rigidbody cached_RB;
    Rigidbody RB => cached_RB ??= GetComponent<Rigidbody>();
    void FixedUpdate()
    {

        var targetPosition = InputTargetPosition;
        foreach (var modifier in TargetPositionModifiers)
            targetPosition = modifier.Modify(targetPosition);

        var velocity = Vector2.zero;
        foreach (var boidMember in BoidGroup.Values.Where(boidMember => boidMember.boidID != this.boidID))
        {
            var delta = (Vector2)(transform.position - boidMember.transform.position);
            velocity += Mathf.Pow(1f / delta.magnitude, 5f) * delta * boidForceMultiplier * Speed;
        }
        velocity += Vector2.ClampMagnitude(targetPosition - transform.position, 1f) * Speed;
        velocity = Vector2.ClampMagnitude(velocity, Speed);
        RB.velocity = velocity;
    }
}
