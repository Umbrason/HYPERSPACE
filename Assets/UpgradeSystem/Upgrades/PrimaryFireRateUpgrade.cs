
public class PrimaryFireRateUpgrade : Upgrade
{
    public SingleShot PrimaryFire;
    public float multiplier;
    public override void Apply()
    {
        PrimaryFire.Firerate *= multiplier;
    }
}
