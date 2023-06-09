
using UnityEngine;

public class BarrelRoll : MonoBehaviour
{
    private SpaceshipMove cached_move;
    private SpaceshipMove Move => cached_move ??= GetComponent<SpaceshipMove>();
    [SerializeField] private AnimationCurve rollCurve;
    public float Duration = .5f;
    public float Distance = 3f;

    void Start()
    {
        SpaceshipInputHandler.OnTriggerRoll += DoABarrelRoll;
        SpaceshipInputHandler.OnMoveChanged += ChangeInputDirection;
    }

    void OnDestroy()
    {
        SpaceshipInputHandler.OnTriggerRoll -= DoABarrelRoll;
        SpaceshipInputHandler.OnMoveChanged -= ChangeInputDirection;
    }

    private Vector2 inputDirection;
    private void ChangeInputDirection(Vector2 input)
    {
        inputDirection = input;
    }

    private void DoABarrelRoll()
    {
        if (!enabled || Rolling) return;
        rollStart = Time.time;
        rollDirection = inputDirection.normalized;
    }


    private bool Rolling => Time.time <= rollStart + Duration;
    private float rollStart;
    private Vector2 rollDirection;


    void FixedUpdate()
    {
        if (Move) Move.enabled = !Rolling;
        if (!Rolling) return;
        var t = (Time.time - rollStart) / Duration;
        var rz = 360 * rollCurve.Evaluate(t) * -Mathf.Sign(rollDirection.x);

        transform.position += (Vector3)rollDirection * Distance / Duration * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rz);
    }
}
