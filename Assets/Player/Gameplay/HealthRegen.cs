
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    public int RegenAmount = 1;
    [SerializeField] private Health playerHealth;
    [SerializeField] private WaveManager waveManager;

    void Start()
    {
        waveManager.OnWaveFinished += ApplyRegen;
    }

    void ApplyRegen(WaveManager.ContinueWaves continueCallback)
    {
        playerHealth.Heal(RegenAmount);
        continueCallback?.Invoke();
    }

    void OnDestroy()
    {
        waveManager.OnWaveFinished -= ApplyRegen;
    }
}
