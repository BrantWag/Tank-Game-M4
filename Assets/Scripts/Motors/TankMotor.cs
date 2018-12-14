using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    [HideInInspector] public TankData data;

    private void Start()
    {
        data = GetComponent<TankData>();
    }
    // Reduce cooldowns
    private void Update()
    {
        if (data.missileCooldownCurrent >= 0)
        {
            data.missileCooldownCurrent -= Time.deltaTime;
        }
        if (data.bulletCooldownCurrent >= 0)
        {
            data.bulletCooldownCurrent -= Time.deltaTime;
        }
        if (data.noiseLevel >= 0)
        {
            data.noiseLevel -= data.noiseLevelReducPerSec;
        }
    }

    // move character 
    public void move(Vector3 movement)
    {
        transform.Translate(movement);
        data.noiseLevel = data.moveNoiseLevel;
    }

    // rotate character
    public void rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
        data.noiseLevel = data.rotateNoiseLevel;
    }

    // check the cooldown of skills
    bool checkCooldown(float cooldown)
    {
        if (cooldown >= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // shoots bullets
    public void ShootBullet()
    {
        // check if the cooldown 
        if (checkCooldown(data.bulletCooldownCurrent))
        {
            // set cooldown to shooting rate
            data.bulletCooldownCurrent = data.bulletCooldownMax;
            // creates a bullet 
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<ProjectileData>().shooterName = this.gameObject.GetComponent<TankData>();
            // Set noiseLevel
            data.noiseLevel = data.bulletNoiseLevel;
            if (data.shellFire != null)
            {
                data.soundSource.clip = data.shellFire;
                data.soundSource.Play();
            }
        }
    }

    // Everything inside works like the above function
    public void ShootMissile()
    {
        if (checkCooldown(data.missileCooldownCurrent))
        {
            data.missileCooldownCurrent = data.missileCooldownMax;
            var missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            missile.GetComponent<ProjectileData>().shooterName = this.gameObject.GetComponent<TankData>();
        }
        if (data.cannonFire != null)
        {
            data.soundSource.clip = data.cannonFire;
            data.soundSource.Play();
        }
        data.noiseLevel = data.missileNoiseLevel;
    }

    // Rotates towards passed in vector
    public void rotateTowards(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotationSpeed * Time.deltaTime);
        data.noiseLevel = data.rotateNoiseLevel;
    }
}
