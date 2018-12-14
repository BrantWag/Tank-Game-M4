using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public GameObject spawnedPickup;

    public Vector3 offset;
    public float spawnTimeMax;
    public float spawnTimeCurrent;
    private Transform tf;

	// Use this for initialization
	void Start ()
    {
        tf = GetComponent<Transform>();
	}

    public void respawnPickups()
    {
        if (spawnedPickup == null)
        {
            if (spawnTimeCurrent >= 0)
            {
                spawnTimeCurrent -= Time.deltaTime;
            }
        }
        if (spawnTimeCurrent <= 0)
        {
            spawnTimeCurrent = spawnTimeMax;
            spawnedPickup = Instantiate(pickupPrefab, tf.position + offset, pickupPrefab.transform.rotation);
            spawnedPickup.transform.SetParent(GameManager.instance.pickupsHolder);
            GameManager.instance.spawnedItems.Add(spawnedPickup);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        respawnPickups();
    }
}
