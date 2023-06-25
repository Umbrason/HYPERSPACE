public interface IPlayerAbilityWithCooldown
{
    public float CurrentCooldown { get; }
    public float CooldownDuration { get; }
    public bool InUse { get; }
}