using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotor : MonoBehaviour
{
    [HideInInspector] public ProjectileData data;
    [HideInInspector] public Rigidbody rb;

    public GameObject explosionEffect;
    public float deleteDelay;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        data = GetComponent<ProjectileData>();

        // Destroy this projectile after a amount of time 
        Destroy(this.gameObject, data.projectileLifespan);
        pushForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        // check to see if the target is either  player or enemy tank
        if (GameManager.instance.players.Contains(other.gameObject.GetComponent<TankData>()) ||
            GameManager.instance.aiUnits.Contains(other.gameObject.GetComponent<TankData>()))
        {
            // if it is then reduce their health
            TankHealth taregtHit = other.gameObject.GetComponent<TankHealth>();
            taregtHit.reduceCurrentHealth(data.projectileDamage, data.shooterName);
        }
        if (data.Hit != null)
        {
            AudioSource.PlayClipAtPoint(data.Hit, transform.position);
        }
        // destroy the projectile
        Destroy(this.gameObject);
    }


    // Propels the projectile forward
    public void pushForward()
    {
        rb.AddForce(transform.forward * data.projectileSpeed);
    }
}
