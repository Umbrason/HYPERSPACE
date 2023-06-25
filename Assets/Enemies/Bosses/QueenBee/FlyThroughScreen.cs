using UnityEngine;

public class FlyThroughScreen : MonoBehaviour, IBossAbility
{
    public Transform TargetIndicator;
    public Transform Visuals;
    public Rigidbody rb;
    public Collider col;

    private Vector3 TargetPos;
    private Vector3 StartPos;
    private Vector3 EndPos;
    private float ChargeDuration = 1f;
    private float ChargeStartTime = float.MinValue;
    bool Charging => Time.fixedTime - ChargeStartTime <= ChargeDuration;
    private float FlyDuration = .5f;


    float TimeSinceFlightStart => Time.fixedTime - (ChargeStartTime + ChargeDuration);
    bool Flying => TimeSinceFlightStart >= 0 && TimeSinceFlightStart <= FlyDuration;

    public float Radius = 5f;
    public int Damage = 3;

    private float CooldownDuration = 4f;

    public bool CanUse => true;
    private float TimeSinceFlightEnd => Time.fixedTime - (ChargeStartTime + ChargeDuration + FlyDuration);
    public float RemainingCooldown => Mathf.Max(CooldownDuration - TimeSinceFlightEnd, 0);

    private bool m_flying;
    void FixedUpdate()
    {
        if (!Charging && !Flying && m_flying)
        {
            TargetIndicator.gameObject.SetActive(false);
            Visuals.localPosition = Vector3.zero;
            Visuals.localRotation = Quaternion.identity;
            FinishedCallback?.Invoke();
            rb.isKinematic = false;
            col.enabled = true;
            m_flying = false;
        }

        if (!Charging && !Flying) return;
        TargetIndicator.gameObject.SetActive(true);

        if (!Flying) return;
        col.enabled = false;
        var t = (Time.fixedTime - (ChargeStartTime + ChargeDuration)) / FlyDuration;

        Visuals.up = EndPos - StartPos;
        Visuals.position = Vector3.Lerp(StartPos, EndPos, t);
        var hits = Physics.OverlapSphere(TargetPos, Radius);
        foreach (var hit in hits)
        {
            var recievers = hit.GetEnabledDamageRecievers();
            foreach (var reciever in recievers)
                reciever.OnHit(Damage);
        }
    }

    private void PickTargetPositions()
    {
        var normalized = GetNormalizedTargetPos();
        var nearBounds = Geometry.ScreenWorldBounds(0);

        TargetPos = new(Mathf.Lerp(nearBounds.min.x, nearBounds.max.x, normalized.x),
                        Mathf.Lerp(nearBounds.min.y, nearBounds.max.y, normalized.y),
                        0);
        EndPos = transform.position;
        StartPos = 2 * TargetPos - EndPos;
    }

    private Vector2 GetNormalizedTargetPos()
    {
        return new Vector2(Random.Range(.2f, .8f), Random.Range(.2f, .8f));
    }

    private System.Action FinishedCallback;
    public void OnUse(System.Action FinishedCallback)
    {
        this.FinishedCallback = FinishedCallback;
        m_flying = true;
        rb.isKinematic = true;
        PickTargetPositions();
        TargetIndicator.position = TargetPos;
        TargetIndicator.localScale = Vector3.one * (Radius * 2f + 1);
        ChargeStartTime = Time.fixedTime;
    }
}
