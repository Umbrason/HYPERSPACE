using UnityEngine;

[RequireComponent(typeof(BoidMovement))]
public class ExplosiveMine : MonoBehaviour, IDamageReciever
{
    float DeploymentDuration = 3f;
    float Fusetime = 1f;
    float MaxLifetime = 10f;
    float ExplosionRadius = 5f;

    private BoidMovement cached_boidMovement;
    private BoidMovement BoidMovement => cached_boidMovement ??= GetComponent<BoidMovement>();

    public Vector2 TargetLocation { get; set; }
    void Start()
    {
        spawnTime = Time.fixedTime;
        triggerTime = spawnTime + MaxLifetime - Fusetime;
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
        if (Time.fixedTime > triggerTime + Fusetime) Explode();
    }

    void Explode()
    {
        Destroy(this);
        Destroy(gameObject);
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
