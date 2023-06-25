
public class RollDistanceUpgrade : Upgrade
{
    public BarrelRoll BarrelRoll;
    public int increase;
    public override void Apply()
    {
        BarrelRoll.Distance += increase;
    }
}
