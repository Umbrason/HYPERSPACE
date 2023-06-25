
using UnityEngine;

public class BarrelRoll : MonoBehaviour, IPlayerAbilityWithCooldown
{
    private SpaceshipMove cached_move;
    private SpaceshipMove Move => cached_move ??= GetComponent<SpaceshipMove>();
    [SerializeField] private AnimationCurve rollCurve;
    public float CurrentCooldown => Mathf.Clamp(CooldownDuration - (Time.fixedTime - (rollStart + Duration)), 0, CooldownDuration);
    public bool InUse => Rolling;
    [field: SerializeField] public float CooldownDuration { get; set; } = 1f;
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
        if (!enabled || Rolling || CoolingDown) return;
        rollStart = Time.fixedTime;
        rollDirection = inputDirection.normalized;
    }


    private bool Rolling => Time.fixedTime <= rollStart + Duration;
    private bool CoolingDown => Time.fixedTime <= rollStart + Duration + CooldownDuration;


    private float rollStart = float.MinValue;
    private Vector2 rollDirection;

    void FixedUpdate()
    {
        if (Move) Move.enabled = !Rolling;
        if (!Rolling) return;
        var t = (Time.fixedTime - rollStart) / Duration;
        var rz = 360 * rollCurve.Evaluate(t) * -Mathf.Sign(rollDirection.x);

        transform.position += (Vector3)rollDirection * Distance / Duration * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rz);
    }
}
