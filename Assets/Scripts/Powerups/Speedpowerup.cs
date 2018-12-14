using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Speedpowerup : Powerup
{
    public float speedIncrease;
    public float rotationIncrease;
    public override void OnActivated(TankData data)
    {
        data.movementSpeed += speedIncrease;
        data.rotationSpeed += rotationIncrease;
        base.OnActivated(data);
    }
    public override void OnDeactivated(TankData data)
    {
        data.movementSpeed -= speedIncrease;
        data.rotationSpeed -= rotationIncrease;
        base.OnDeactivated(data);
    }
}