using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    [HideInInspector] public TankData data;

	// Use this for initialization
	void Start ()
    {
        data = GetComponent<TankData>();
	}
    // reduce current health 
    public void reduceCurrentHealth(float damage, TankData attacker)
    {
        data.healthCurrent -= damage;
        checkDeath(attacker);
    }

    // check if tank has died
    public void checkDeath(TankData Dmg)
    {
        if (data.healthCurrent <= 0)
        {
            Dmg.score += GameManager.instance.scorePerKill;
            Destroy(this.gameObject);
        }
    }

    // Reset current health 
    public void resetHealth()
    {
        data.healthCurrent = data.healthMax;
    }

}
