
using UnityEngine;

[RequireComponent(typeof(BoidMovement))]
public class RandomBoidTarget : MonoBehaviour
{
    private Vector2 Target;
    private float targetChangeFrequency = .2f;
    [SerializeField] private BoidMovement boidMovement;


    float lastTargetChange = float.MinValue;
    void FixedUpdate()
    {
        if (!boidMovement) return;
        if (Time.fixedTime - lastTargetChange <= 1 / targetChangeFrequency) return;
        lastTargetChange = Time.fixedTime - Random.value / targetChangeFrequency * .5f;
        var bounds = Geometry.ScreenWorldBounds(transform.position.z);
        boidMovement.InputTargetPosition = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }

}
