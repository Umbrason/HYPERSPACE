using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyWaveData : ScriptableObject
{
    public List<EnemyGroup> WaveMembers;
}