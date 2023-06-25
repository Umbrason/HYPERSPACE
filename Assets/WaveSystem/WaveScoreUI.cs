
using TMPro;
using UnityEngine;

public class WaveScoreUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TMP_Text text;


    void Start()
    {
        waveManager.OnWaveFinished += OnWaveDone;
    }

    void OnWaveDone(WaveManager.ContinueWaves continueCallback)
    {
        text.text = $"Wave {waveManager.WaveID}";
        continueCallback?.Invoke();
    }

    void OnDestroy()
    {
        waveManager.OnWaveFinished -= OnWaveDone;
    }
}
