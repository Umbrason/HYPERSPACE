using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float Cooldown;
    [SerializeField] private float duration = 1f;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform nozzle;
    [SerializeField] private AnimationCurve laserCurve;

    public bool Firing => path != null;



    private Vector2[] path;
    private float pathLength = 0f;
    private float LaserFireStartTime = float.MinValue;
    public void StartLaser(Vector2[] path)
    {
        if (Firing || path.Length < 2) return;
        LaserFireStartTime = Time.time;
        this.path = path;
        this.pathLength = 0f;
        for (int i = 0; i < path.Length - 1; i++) pathLength += (path[i] - path[i + 1]).magnitude;
        UpdateLaser(path[0]);
        lr.enabled = true;
    }

    public void FixedUpdate()
    {
        if (Time.time - LaserFireStartTime > Cooldown && !Firing) StartLaser(PickLaserPath());
        if (!Firing) return;
        var t = (Time.time - LaserFireStartTime) / duration;
        var pos = path.SamplePath(Mathf.Clamp01(laserCurve.Evaluate(t)) * pathLength);
        UpdateLaser(pos);
        if (t >= 1) StopLaser();
    }

    private Vector2[] PickLaserPath()
    {
        return new Vector2[] { new(-5, 1), new(5, 1) };
    }

    private void StopLaser()
    {
        lr.enabled = false;
        path = null;
    }

    private void UpdateLaser(Vector2 targetPos)
    {
        lr.SetPosition(0, nozzle.position);
        lr.SetPosition(1, targetPos);
    }
}