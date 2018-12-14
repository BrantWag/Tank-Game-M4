using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Powerup types
    public Healthpowerup healthPowerup;
    public Speedpowerup speedPowerup;
    public Fireratepowerup firerate;


    // Mats
    public Material[] mats;
    public Material healthMat;
    public Material speedMat;
    public Material fireRateMat;

    public enum PowerupType
    {
        powerupHealth,
        powerupSpeed,
        firerate,
    }
    public PowerupType currentPowerupType;
    public AudioClip sound;


    public void Start()
    {
        // choose a new powerup
        currentPowerupType = (PowerupType)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(PowerupType)).Length);
        mats = GetComponent<MeshRenderer>().materials;
        // Change material
        switch (currentPowerupType)
        {
            case PowerupType.powerupHealth:
            {
                name = "Health Powerup";
                mats[0] = healthMat;
                break;
            }
            case PowerupType.powerupSpeed:
            {
                name = "Speed Powerup";
                mats[0] = speedMat;
                break;
            }
            case PowerupType.firerate:
            {
                name = "Fire Rate";
                mats[0] = fireRateMat;
                break;
            }
        }
        GetComponent<MeshRenderer>().materials = mats;
    }

    public void OnTriggerEnter(Collider other)
    {
        Controller_Powerup buffController = other.GetComponent<Controller_Powerup>();
        if (buffController != null)
        {
            switch (currentPowerupType)
            {
                case PowerupType.powerupHealth:
                {
                    buffController.add(healthPowerup);
                    break;
                }
                case PowerupType.powerupSpeed:
                {
                    buffController.add(speedPowerup);
                    break;
                }
                case PowerupType.firerate:
                {
                    buffController.add(firerate);
                    break;
                }
            }
            if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, transform.position, 1.0f);
            }
            Destroy(gameObject);

        }
        
    }

    public void OnDestroy()
    {
        GameManager.instance.spawnedItems.Remove(this.gameObject);
    }
}
