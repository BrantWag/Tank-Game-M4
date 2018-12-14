
[System.Serializable]
public class Powerup
{
    // durations
    public float buffDurationMax;
    public float buffDurationCurrent;
    public bool isPerm;

    public virtual void OnActivated(TankData data)
    {

    }
    public virtual void OnDeactivated(TankData data)
    {

    }
}
