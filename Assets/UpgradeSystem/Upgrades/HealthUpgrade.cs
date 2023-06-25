
public class HealthUpgrade : Upgrade
{
    public Health health;
    public int increase;
    public override void Apply()
    {
        health.MaxHP += increase;
        health.Heal(increase);
    }
}