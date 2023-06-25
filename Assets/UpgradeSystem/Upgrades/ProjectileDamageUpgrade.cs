
public class ProjectileDamageUpgrade : Upgrade
{
    public Projectile Projectile;
    public int increase;
    public override void Apply()
    {
        Projectile.Damage += increase;
    }
}