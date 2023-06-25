using UnityEngine;

public class SpawnEnemies : MonoBehaviour, IBossAbility
{
    [SerializeField] private GameObject EnemyObject;
    [SerializeField] private Transform SpawnPointTransform;
    [SerializeField] private float SpawnDuration = 5f;
    [SerializeField] private int SpawnAmount = 5;
    [SerializeField] private int SpawnCap = 10;

    private float TimeSinceSpawnEnd => Time.fixedTime - (SpawnStartTime + SpawnDuration);
    private float SpawnStartTime;
    private bool Spawning => Time.fixedTime - SpawnStartTime <= SpawnDuration;

    private float CooldownDuration = 5f;

    public bool CanUse => WaveManager.AliveWaveMembers.Count - 1 < SpawnCap;
    public float RemainingCooldown => Mathf.Max(CooldownDuration - TimeSinceSpawnEnd, 0);

    private int m_AmountSpawned = 0;
    void FixedUpdate()
    {
        if (!Spawning && m_AmountSpawned > 0)
        {
            m_AmountSpawned = 0;
            FinishedCallback?.Invoke();
        }
        if (!Spawning) return;
        var t = (Time.fixedTime - SpawnStartTime) / SpawnDuration;
        var spawnTargetCount = Mathf.CeilToInt(t * SpawnAmount);
        if (spawnTargetCount > m_AmountSpawned)
        {
            var instance = Instantiate(EnemyObject);
            instance.transform.position = new(SpawnPointTransform.position.x,
                                              SpawnPointTransform.position.y,
                                               instance.transform.position.z);
            m_AmountSpawned++;
        }
    }

    private System.Action FinishedCallback;
    public void OnUse(System.Action FinishedCallback)
    {
        this.FinishedCallback = FinishedCallback;
        SpawnStartTime = Time.fixedTime;
    }
}
