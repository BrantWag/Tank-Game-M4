using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    [HideInInspector] private TankData data;
    [HideInInspector] private TankMotor motor;

    public enum controlType
    {
        wasd,
        arrows,
    }
    public controlType selectedController;

    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();

        GameManager.instance.players.Add(this.data);

    }
    void Update()
    {
        switch (selectedController)
        {
            case controlType.wasd:
                wasdControls();
                break;
            case controlType.arrows:
                arrowsControls();
                break;
        }
    }
    private void wasdControls()
    {
        if (Input.GetButton("Fire1"))
        {
            // Shoot machine gun
            motor.ShootBullet();
        }
        if (Input.GetButton("Fire2"))
        {
            // Shoot cannon
            motor.ShootMissile();
        }
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("pressed");
            // Move Forward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(movementVector);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // Move Backward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(-movementVector);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // Move Right
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Move Left
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }
    private void arrowsControls()
    {
        if (Input.GetButton("Fire1_P2"))
        {
            // Shoot machine gun
            motor.ShootBullet();
        }
        if (Input.GetButton("Fire2_P2"))
        {
            // Shoot cannon
            motor.ShootMissile();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Move Forward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(movementVector);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Move Backward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(-movementVector);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Move Right
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Move Left
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }


    private void OnDestroy()
    {
        if (data.died != null)
        {
            AudioSource.PlayClipAtPoint(data.died, transform.position);
        }
        // Add to GameManager List players
        GameManager.instance.players.Remove(this.data);
    }
}