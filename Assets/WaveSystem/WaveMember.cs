
using UnityEngine;

public class WaveMember : MonoBehaviour
{
    public void Start()
    {
        WaveManager.AliveWaveMembers.Add(this);
    }

    public void OnDestroy()
    {
        WaveManager.AliveWaveMembers.Remove(this);
    }
}
