using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_CharacterSpawner : MonoBehaviour
{
    bool inUse;

    public Room section;

	// Use this for initialization
	void Start ()
    {
        GameManager.instance.characterSpawns.Add(this.transform);
        section = GetComponentInParent<Room>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
