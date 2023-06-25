
public class LaserDuration : Upgrade
{
    public PlayerLaser Laser;
    public float increment;
    public override void Apply()
    {
        Laser.Duration += increment;
    }
}
