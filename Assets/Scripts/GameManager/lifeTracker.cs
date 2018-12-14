using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeTracker : MonoBehaviour
{
    public Image[] lives;
    public TankData data;

    // Use this for initialization
    void Start()
    {
        lives = GetComponentsInChildren<Image>();
        data = GetComponentInParent<TankData>();
    }

    // Display lives according to how many lives the tank has
    // Update is called once per frame
    void Update()
    {
        if (data.lives == 3)
        {
            lives[0].enabled = true;
            lives[1].enabled = true;
            lives[2].enabled = true;
        }
        if (data.lives == 2)
        {
            lives[0].enabled = false;
            lives[1].enabled = true;
            lives[2].enabled = true;
        }
        if (data.lives == 1)
        {
            lives[0].enabled = false;
            lives[1].enabled = false;
            lives[2].enabled = true;
        }
        if (data.lives <= 0)
        {
            lives[0].enabled = false;
            lives[1].enabled = false;
            lives[2].enabled = false;
        }
    }
}
