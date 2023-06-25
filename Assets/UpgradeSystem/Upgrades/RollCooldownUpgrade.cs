
public class RollCooldownUpgrade : Upgrade
{
    public BarrelRoll BarrelRoll;
    public float multiplier;
    public override void Apply()
    {
        BarrelRoll.CooldownDuration *= multiplier;
    }
}
