
public class LaserDamage : Upgrade
{
    public PlayerLaser Laser;
    public int increment;
    public override void Apply()
    {
        Laser.Damage += increment;
    }
}
