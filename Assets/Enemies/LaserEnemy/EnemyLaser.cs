using System.Linq;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float Cooldown = 1;
    public int Damage = 1;
    [SerializeField] private float FireDuration = 1f;
    [SerializeField] private float ChargeDuration = 1f;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private LineRenderer pathPreview;
    [SerializeField] private Transform nozzle;
    [SerializeField] private AnimationCurve laserCurve;
    [SerializeField] private GameObject laserImpactVFX;

    private bool pathLocked;
    public Vector2[] Path
    {
        get => m_path;
        set
        {
            if (m_path == value || pathLocked) return;
            m_path = value;
            m_pathLength = 0f;
            for (int i = 0; i < m_path.Length - 1; i++) m_pathLength += (m_path[i] - m_path[i + 1]).magnitude;
        }
    }

    private Vector2[] m_path;
    private float m_pathLength;

    void Start()
    {
        LaserFireStartTime = Time.fixedTime - FireDuration;
    }

    private bool charging;
    private float LaserTriggerTime;
    private void ChargeLaser()
    {
        LaserTriggerTime = Time.fixedTime;
        charging = true;
        Path = PickLaserPath();
        pathLocked = true;
        pathPreview.enabled = true;
        pathPreview.SetPositions(Path.Select(v => (Vector3)v).ToArray());
        pathPreview.material.SetFloat("_Length", m_pathLength);
    }

    private bool firing;
    private float LaserFireStartTime;
    private void StartLaser()
    {
        charging = false;
        firing = true;
        LaserFireStartTime = Time.fixedTime;
        laserImpactVFX.SetActive(true);
        lastPos = Path[0];
        lr.enabled = true;
        UpdateLaser();
    }

    private Vector2 lastPos;
    private void UpdateLaser()
    {
        var t = (Time.time - LaserFireStartTime) / FireDuration;
        var tCurve = laserCurve.Evaluate(t);
        var pos = Path.SamplePath(Mathf.Clamp01(tCurve) * m_pathLength);

        //Visuals
        pathPreview.SetPositions(Path.RemoveFromStart(tCurve * m_pathLength).Select(v => (Vector3)v).ToArray());
        pathPreview.material.SetFloat("_Length", (1 - tCurve) * m_pathLength);
        lr.SetPosition(0, nozzle.position);
        lr.SetPosition(1, pos);
        laserImpactVFX.transform.position = pos;

        //Damage           
        var dir = pos - lastPos;
        var laserTargets = Physics.SphereCastAll(lastPos, .5f, dir, dir.magnitude);
        lastPos = pos;
        foreach (var target in laserTargets)
        {
            var damageRecievers = target.collider.GetEnabledDamageRecievers();
            foreach (var damageReciever in damageRecievers) damageReciever.OnHit(Damage);
        }
        //EndCondition
        if (t >= 1) StopLaser();
    }

    private void StopLaser()
    {
        firing = false;
        laserImpactVFX.SetActive(false);
        lr.enabled = false;
        pathPreview.enabled = false;
        Path = null;
        pathLocked = false;
    }

    private bool OffCooldown => LaserFireStartTime + FireDuration + Cooldown <= Time.fixedTime;
    private void FixedUpdate()
    {
        if (!(charging || firing) && OffCooldown) ChargeLaser();
        if (charging && Time.fixedTime >= LaserTriggerTime + ChargeDuration) StartLaser();
        if (!firing) return;
        UpdateLaser();
    }

    private Vector2[] PickLaserPath()
    {
        var horizontal = Random.value >= .5f;
        (Vector3 screenMin, Vector3 screenMax) = Geometry.ScreenWorldBounds(0);
        Vector2 start;
        Vector2 end;
        switch (horizontal)
        {
            case true:
                start = new(screenMin.x, Random.Range(screenMin.y, screenMax.y));
                end = new(screenMax.x, Random.Range(screenMin.y, screenMax.y));
                break;
            case false:
                start = new(Random.Range(screenMin.x, screenMax.x), screenMin.y);
                end = new(Random.Range(screenMin.x, screenMax.x), screenMax.y);
                break;
        }
        return new[] { start, end };
    }
}