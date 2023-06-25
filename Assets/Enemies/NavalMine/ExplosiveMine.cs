using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoidMovement), typeof(EnemyMaterialAnimator))]
public class ExplosiveMine : MonoBehaviour, IDamageReciever
{
    float DeploymentDuration = 3f;
    float Fusetime = .75f;
    float MaxLifetime = 10f;
    float ExplosionRadius = 5f;
    int ExplosionDamage = 3;

    [SerializeField] private GameObject AVFX;
    [SerializeField] private GameObject Debris;
    [SerializeField] private GameObject Visuals;
    [SerializeField] private AudioSource TriggerSFXSource;


    private BoidMovement cached_boidMovement;
    private BoidMovement BoidMovement => cached_boidMovement ??= GetComponent<BoidMovement>();

    private EnemyMaterialAnimator cached_EnemyMaterialAnimator;
    private EnemyMaterialAnimator EnemyMaterialAnimator => cached_EnemyMaterialAnimator ??= GetComponent<EnemyMaterialAnimator>();

    public Vector2 TargetLocation { get; set; }
    void Start()
    {
        spawnTime = Time.fixedTime;
        triggerTime = spawnTime + (Random.value + 1) * MaxLifetime / 2f - Fusetime;
        spawnLocation = transform.position;
        BoidMovement.GroupIndex = 1;
        BoidMovement.InputTargetPosition = spawnLocation;
    }
    private float spawnTime;
    private Vector3 spawnLocation;
    private float triggerTime;

    private bool IsDeploying => spawnTime + DeploymentDuration > Time.fixedTime;
    private bool IsTriggered => triggerTime <= Time.fixedTime;

    void FixedUpdate()
    {
        if (IsDeploying)
        {
            var t = (Time.fixedTime - spawnTime) / DeploymentDuration;
            EnemyMaterialAnimator.SetEmissionBrightness(t);
        }
        if (IsTriggered)
        {
            if (!TriggerSFXSource.isPlaying) TriggerSFXSource.Play();
            var t = (Time.fixedTime - triggerTime) / Fusetime;
            EnemyMaterialAnimator.SetEmissionBrightness(Mathf.Cos(t * 3.141f * 6f) / 2f + .5f);
        }
        if (Time.fixedTime > triggerTime + Fusetime) Explode();
    }

    void Explode()
    {
        Destroy(this);
        AVFX.SetActive(true);
        Debris.SetActive(true);


        var targets = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (var collider in targets)
        {
            var damageRecievers = collider.GetEnabledDamageRecievers();
            foreach (var damageReciever in damageRecievers) damageReciever.OnHit(ExplosionDamage);
        }

        Destroy(Visuals);
        Destroy(gameObject, 2f);
    }

    void Ignite()
    {
        if (IsDeploying || IsTriggered) return; //still deploying
        triggerTime = Time.fixedTime;
    }



    void OnCollisionEnter()
    {
        Ignite();
    }

    public void OnHit(int damage)
    {
        Ignite();
    }
}
