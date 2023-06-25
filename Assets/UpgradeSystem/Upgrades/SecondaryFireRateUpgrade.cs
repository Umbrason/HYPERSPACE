
public class SecondaryFireRateUpgrade : Upgrade
{
    public MultiShot SecondaryFire;
    public float multiplier;
    public override void Apply()
    {
        SecondaryFire.CooldownDuration *= multiplier;
    }
}
