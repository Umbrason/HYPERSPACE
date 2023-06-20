
using UnityEngine;

public class Turbulence : MonoBehaviour, ITargetPositionModifier
{
    private float seed = -1;
    void Start() => seed = Random.value * 1000f;
    public float frequency = .2f;
    public float amplitude = 4f;

    public Vector3 Modify(Vector3 target)
    {
        var dx = Mathf.PerlinNoise(seed, Time.time * frequency) - .5f;
        var dy = Mathf.PerlinNoise(seed, -Time.time * frequency) - .5f;
        return target + new Vector3(dx, dy) * amplitude * 2f;
    }
}
