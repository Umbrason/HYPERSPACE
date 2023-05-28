using System.Collections.Generic;
using UnityEngine;

public class ThrusterRenderer : MonoBehaviour
{
    public float length = 5f;
    public Vector3 velocity = Vector3.back;
    public AnimationCurve WidthTaper = AnimationCurve.Constant(0, 1, 1);
    private readonly List<Vector3> positions = new();

    [SerializeField] private LineRenderer lr;

    public void Update()
    {
        if (!lr) return;
        lr.positionCount = positions.Count;
        lr.SetPositions(positions.ToArray());
        lr.widthCurve = WidthTaper;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < positions.Count; i++)
            positions[i] += velocity * Time.fixedDeltaTime;
        positions.Insert(0, transform.position);
        while (positions.Count > 0 && (positions[positions.Count - 1] - transform.position).sqrMagnitude > length * length)
            positions.RemoveAt(positions.Count - 1);
    }
}
