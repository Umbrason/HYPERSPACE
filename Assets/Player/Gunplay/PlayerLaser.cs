using UnityEngine;
[RequireComponent(typeof(PlayerAim))]
public class PlayerLaser : MonoBehaviour, IPlayerAbilityWithCooldown
{
    public float Tickrate = .1f;
    public float Duration = 5f;
    public int Damage = 1;

    private PlayerAim cached_PlayerAim;
    private PlayerAim PlayerAim => cached_PlayerAim ??= GetComponent<PlayerAim>();

    public float CurrentCooldown => CooldownDuration - Mathf.Clamp(Time.fixedTime - LaserFireStartTime - Duration, 0, CooldownDuration);
    public float CooldownDuration { get; set; } = 30f;
    public bool InUse => CurrentCooldown > CooldownDuration - .1f;

    [SerializeField] private AudioSource SFX;
    [SerializeField] private Transform nozzle;
    [SerializeField] private GameObject laserImpactVFX;
    [SerializeField] private LineRenderer lr;

    public void Start()
    {
        SpaceshipInputHandler.OnTriggerUltimate += StartLaser;
        LaserFireStartTime = -Duration;
    }

    public void OnDestroy()
    {
        SpaceshipInputHandler.OnTriggerUltimate -= StartLaser;
    }

    private int nozzleIndex = 0;
    void FixedUpdate()
    {
        if (firing)
        {
            UpdateLaser();
            if (Time.fixedTime - LaserFireStartTime > Duration)
                StopLaser();
        }

    }

    private bool firing;
    private float LaserFireStartTime;
    private void StartLaser()
    {
        if (Time.fixedTime < LaserFireStartTime + Duration + CooldownDuration) return;
        firing = true;
        LaserFireStartTime = Time.fixedTime;
        laserImpactVFX?.SetActive(true);
        lastPos = PlayerAim.targetPosition;
        lr.enabled = true;
        UpdateLaser();
    }

    private void StopLaser()
    {
        firing = false;
        laserImpactVFX?.SetActive(false);
        lr.enabled = false;
    }

    private Vector3 lastPos;
    private float LastDamageTick = float.MinValue;
    private void UpdateLaser()
    {
        var pos = PlayerAim.targetPosition;

        //Visuals        
        lr.SetPosition(0, nozzle.position);
        lr.SetPosition(1, pos);
        if (laserImpactVFX) laserImpactVFX.transform.position = pos;

        if (Time.fixedTime - LastDamageTick < Tickrate) return;
        LastDamageTick = Time.fixedTime;
        //Damage           
        var dir = pos - lastPos;
        var laserTargets = Physics.SphereCastAll(lastPos, .5f, dir, dir.magnitude);
        lastPos = pos;
        foreach (var target in laserTargets)
        {
            var damageRecievers = target.collider.GetEnabledDamageRecievers();
            foreach (var damageReciever in damageRecievers) damageReciever.OnHit(Damage);
        }
    }
}
