using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Fireratepowerup : Powerup
{
    public float Firerate;
    public override void OnActivated(TankData data)
    {
        data.missileCooldownMax -= Firerate;
        base.OnActivated(data);
    }
    public override void OnDeactivated(TankData data)
    {
        data.missileCooldownMax += Firerate;
        base.OnDeactivated(data);
    }
}
