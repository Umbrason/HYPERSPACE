using System;

public interface IBossAbility
{
    bool CanUse { get; }
    float RemainingCooldown { get; }
    void OnUse(Action FinishedCallback);
}
