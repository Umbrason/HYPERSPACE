
public class LaserCooldown : Upgrade
{
    public PlayerLaser Laser;
    public float multiplier;
    public override void Apply()
    {
        Laser.CooldownDuration *= multiplier;
    }
}
