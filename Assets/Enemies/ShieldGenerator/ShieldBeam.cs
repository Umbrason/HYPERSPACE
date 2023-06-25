using System;
using UnityEngine;

public class ShieldBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform Nozzle;

    [SerializeField] private ShieldBeamTarget target;
    [SerializeField] private Transform Visuals;

    private Guid guid = Guid.NewGuid();

    public void FixedUpdate()
    {
        lr.enabled = target != null;
        if (target == null) PickTarget();
        if (target == null) return;
        var delta = Nozzle.position - target.transform.position;
        Visuals.up = Visuals.position - target.transform.position;
        lr.SetPositions(new[] { Nozzle.position, target.transform.position });
        lr.material.SetFloat("_Length", delta.magnitude);
    }

    private void PickTarget()
    {
        if (ShieldBeamTarget.AvailableTargets.Count == 0) return;
        target = ShieldBeamTarget.AvailableTargets[UnityEngine.Random.Range(0, ShieldBeamTarget.AvailableTargets.Count)];
        target.AddShieldSource(guid);
    }

    private void OnDestroy()
    {
        target.RemoveShieldSource(guid);
    }
}
