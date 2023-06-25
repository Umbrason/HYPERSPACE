
public class SecondaryProjectileCount : Upgrade
{
    public MultiShot Secondary;
    public int increase;
    public override void Apply()
    {
        Secondary.ShotCount += increase;
    }
}