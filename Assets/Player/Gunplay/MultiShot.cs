using UnityEngine;

[RequireComponent(typeof(PlayerAim))]
public class MultiShot : MonoBehaviour, IPlayerAbilityWithCooldown
{
    public bool Input { get; set; }
    public int ShotCount = 3;
    public float SpreadRadius = 1f;

    private PlayerAim cached_PlayerAim;
    private PlayerAim PlayerAim => cached_PlayerAim ??= GetComponent<PlayerAim>();

    public float CurrentCooldown => CooldownDuration - Mathf.Clamp(Time.fixedTime - lastShot, 0, CooldownDuration);
    [field: SerializeField] public float CooldownDuration { get; set; } = 8f;
    public bool InUse => CurrentCooldown > .9f * CooldownDuration;

    [SerializeField] private Projectile ProjectileTemplate;
    [SerializeField] private AudioSource SFXClip;
    [SerializeField] private Transform[] nozzles = new Transform[0];

    private void SetInput(bool value) => Input = value;
    public void Start()
    {
        SpaceshipInputHandler.OnSecondaryFireChanged += SetInput;
    }

    public void OnDestroy()
    {
        SpaceshipInputHandler.OnSecondaryFireChanged -= SetInput;
    }

    private int nozzleIndex = 0;
    private float lastShot = float.MinValue;
    void FixedUpdate()
    {
        if (!Input) return;
        if (Time.fixedTime < lastShot + CooldownDuration) return;
        lastShot = Time.fixedTime;
        Shoot();
    }

    private void Shoot()
    {
        SFXClip.PlayOneShot(SFXClip.clip);
        nozzleIndex = (nozzleIndex + 1) % nozzles.Length;
        for (int i = 0; i < ShotCount; i++)
        {
            var projectileInstance = Instantiate(ProjectileTemplate);
            projectileInstance.transform.position = nozzles[nozzleIndex].position;
            var offsetAngle = i / (float)ShotCount * Mathf.PI * 2;
            var targetOffset = new Vector2(Mathf.Cos(offsetAngle), Mathf.Sin(offsetAngle)) * SpreadRadius;
            projectileInstance.TargetPosition = PlayerAim.targetPosition + (Vector3)targetOffset;

        }
    }
}
