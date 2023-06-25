
public class RegenUpgrade : Upgrade
{
    public HealthRegen regen;
    public int increase;
    public override void Apply()
    {
        regen.RegenAmount += increase;
    }
}