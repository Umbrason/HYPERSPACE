using UnityEngine;

[RequireComponent(typeof(PlayerAim))]
public class SingleShot : MonoBehaviour, IPlayerAbilityWithCooldown
{
    public bool Input { get; set; }
    public float Firerate = 1f;

    private PlayerAim cached_PlayerAim;
    private PlayerAim PlayerAim => cached_PlayerAim ??= GetComponent<PlayerAim>();

    public float CurrentCooldown => CooldownDuration - Mathf.Clamp(Time.fixedTime - lastShot, 0, 1f / Firerate);
    public float CooldownDuration => 1f / Firerate;
    public bool InUse => CurrentCooldown > CooldownDuration - .1f;

    [SerializeField] private Projectile ProjectileTemplate;
    [SerializeField] private AudioSource SFXClip;
    [SerializeField] private Transform[] nozzles = new Transform[0];

    private void SetInput(bool value) => Input = value;
    public void Start()
    {
        SpaceshipInputHandler.OnPrimaryFireChanged += SetInput;
    }

    public void OnDestroy()
    {
        SpaceshipInputHandler.OnPrimaryFireChanged -= SetInput;
    }

    private int nozzleIndex = 0;
    private float lastShot = float.MinValue;
    void FixedUpdate()
    {
        if (!Input) return;
        if (Time.fixedTime < lastShot + 1f / Firerate) return;
        lastShot = Time.fixedTime;
        Shoot();
    }

    private void Shoot()
    {
        SFXClip.PlayOneShot(SFXClip.clip);
        nozzleIndex = (nozzleIndex + 1) % nozzles.Length;
        var projectileInstance = Instantiate(ProjectileTemplate);
        projectileInstance.transform.position = nozzles[nozzleIndex].position;
        projectileInstance.TargetPosition = PlayerAim.targetPosition;
    }
}
