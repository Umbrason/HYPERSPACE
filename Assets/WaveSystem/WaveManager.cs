
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemyWaveData[] Waves;
    public EnemyWaveData[] BossWaves;
    private const int bossInterval = 5;
    public static HashSet<WaveMember> AliveWaveMembers = new();

    public event Action<ContinueWaves> OnWaveFinished;
    private HashSet<Guid> WavePauseHandles = new();
    public delegate void ContinueWaves();

    private int waveID = 0;
    public int WaveID => waveID;
    private bool isBossWave => waveID % 5 == 0;
    private int waveIndex => Mathf.Clamp(waveID - Mathf.FloorToInt(waveID / 5f), 1, Waves.Length);
    private int waveDifficulty => waveID / 5;

    private int lastFinishedWave = 0;
    void FixedUpdate()
    {
        if (AliveWaveMembers.Count == 0 && lastFinishedWave < waveID)
        {
            var invocations = OnWaveFinished?.GetInvocationList();
            if (invocations != null)
                foreach (var invocation in invocations)
                {
                    var guid = Guid.NewGuid();
                    WavePauseHandles.Add(guid);
                    invocation.DynamicInvoke((ContinueWaves)(() => WavePauseHandles.Remove(guid)));
                }
            lastFinishedWave = waveID;
        }
        if (WavePauseHandles.Count > 0) return;
        if (lastFinishedWave >= waveID)
        {
            waveID++;
            PlayerPrefs.SetInt("Highscore", waveID);
            var wave = isBossWave ? BossWaves[UnityEngine.Random.Range(0, BossWaves.Length)] : Waves[waveIndex - 1];
            SpawnWave(wave);
        }
    }

    private void SpawnWave(EnemyWaveData data)
    {
        Debug.Log($"Spawning {data.name} as wave {waveID}");
        var enemyGroups = data.WaveMembers;
        if (isBossWave)
        {
            var group = enemyGroups.RandomElement();
            foreach (var enemy in group.Prefabs)
            {
                var instance = Instantiate(enemy);
                var (boundsMin, boundsMax) = Geometry.ScreenWorldBounds(instance.transform.position.z);
                instance.transform.position = new(UnityEngine.Random.Range(boundsMin.x, boundsMax.x), UnityEngine.Random.Range(boundsMin.y, boundsMax.y), boundsMin.z);
            }
            return;
        }
        for (int i = 0; i <= waveDifficulty; i++)
        {
            var group = enemyGroups.RandomElement();
            foreach (var enemy in group.Prefabs)
            {
                var instance = Instantiate(enemy);
                var (boundsMin, boundsMax) = Geometry.ScreenWorldBounds(instance.transform.position.z);
                instance.transform.position = new(UnityEngine.Random.Range(boundsMin.x, boundsMax.x), UnityEngine.Random.Range(boundsMin.y, boundsMax.y), boundsMin.z);
            }
        }
    }
}
