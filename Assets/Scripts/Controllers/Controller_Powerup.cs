using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Powerup : MonoBehaviour
{
    public TankData data;
    public List<Powerup> buffs;
    public List<Powerup> buffDuration;

	// Use this for initialization
	void Start ()
    {
        buffs = new List<Powerup>();
        data = GetComponent<TankData>();
        buffDuration = new List<Powerup>();
	}

    public void add(Powerup pu) //pu=powerup
    {
        pu.OnActivated(data);
        pu.buffDurationCurrent = pu.buffDurationMax;
        if (pu.isPerm == false)
        {
            buffs.Add(pu);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        foreach (Powerup buff in buffs)
        {
            buff.buffDurationCurrent -= Time.deltaTime;
            if (buff.buffDurationCurrent <= 0)
            {
                buffDuration.Add(buff);
            }
        }
        foreach (Powerup expiredBuff in buffDuration)
        {
            buffs.Remove(expiredBuff);
            expiredBuff.OnDeactivated(data);
        }
        buffDuration.Clear();
	}

}
