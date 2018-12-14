using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;

    public GameObject powerupSpawner;
    public GameObject characterSpawner;

    public List<Transform> waypoints;
}
