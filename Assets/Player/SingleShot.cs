using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(PlayerAim))]
public class SingleShot : MonoBehaviour
{
    public bool Input { get; set; }
    public float Firerate = 1f;

    private PlayerAim cached_PlayerAim;
    private PlayerAim PlayerAim => cached_PlayerAim ??= GetComponent<PlayerAim>();

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
    private float lastShot = -1f;
    void FixedUpdate()
    {
        if (!Input) return;
        if (Time.time < lastShot + 1f / Firerate) return;
        lastShot = Time.time;
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
