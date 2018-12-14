using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI : MonoBehaviour
{
    
    [HideInInspector] public TankData data;
    [HideInInspector] public TankMotor motor;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;

    public float timeInFlee;
    public float timeToFlee;

    public float HowClose;
    public float distanceToMaintain;
    public float distanceToMaintainHG;
    public float distanceToMaintainAggro;
    public float distanceToMaintainCreeper;
    public float distanceToMaintainScared;

    public float skittishShootingAngle;

    public MeshRenderer topColor;
    public MeshRenderer leftColor;
    public MeshRenderer rightColor;
    public MeshRenderer bottomColor;

    [HideInInspector]public enum personalities
    {
        creeper,
        scared,
        holdGround,
        aggro
    };
    public personalities personality;

	// Use this for initialization
	void Start ()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        GameManager.instance.aiUnits.Add(this.data);

       //randomly select a personality 

       personality = (personalities)Random.Range(0, System.Enum.GetNames(typeof(personalities)).Length);
      
       switch (personality)
       {
            case personalities.aggro:
                gameObject.AddComponent<Aggro_AI>();
                distanceToMaintain = distanceToMaintainAggro;
                topColor.materials[0].color = Color.blue;
                leftColor.materials[0].color = Color.blue;
                rightColor.materials[0].color = Color.blue;
                bottomColor.materials[0].color = Color.blue;
                break;
            case personalities.scared:
                gameObject.AddComponent<Scared_AI>();
                distanceToMaintain = distanceToMaintainScared;
                topColor.materials[0].color = Color.yellow;
                leftColor.materials[0].color = Color.yellow;
                rightColor.materials[0].color = Color.yellow;
                bottomColor.materials[0].color = Color.yellow;
                break;
            case personalities.creeper:
                gameObject.AddComponent<Creeper_AI>();
                distanceToMaintain = distanceToMaintainCreeper;
                topColor.materials[0].color = Color.magenta;
                leftColor.materials[0].color = Color.magenta;
                rightColor.materials[0].color = Color.magenta;
                bottomColor.materials[0].color = Color.magenta;
                break;
            case personalities.holdGround:
                gameObject.AddComponent<HoldsGround_AI>();
                distanceToMaintain = distanceToMaintainHG;
                topColor.materials[0].color = Color.gray;
                leftColor.materials[0].color = Color.grey;
                rightColor.materials[0].color = Color.grey;
                bottomColor.materials[0].color = Color.grey;
                break;
       }
    }
	
    private void OnDestroy()
    {
        if (data.died != null)
        {
            AudioSource.PlayClipAtPoint(data.died, transform.position);
        }
        // Remove from GameManager List players
        GameManager.instance.aiUnits.Remove(this.data);
        GameManager.instance.numAICurrent--;
    }

    public bool canMove()
    {
        //hits nothing return true
        if (Physics.Raycast(transform.position, transform.forward, data.wallDetectDistance))
        {
            return false;
        }
        // else return false
        return true;
    }

    public bool floorExists()
    {
        //nothing return false, floor doesn't exist
        if (Physics.Raycast(transform.position + (transform.forward * data.wallDetectDistance), -transform.up, data.wallDetectDistance))
        {
            return true;
        }
        // else return true
        return false;
    }

    public bool canSeeTarget()
    {
        // Create a vector to target 
        Vector3 vectorToTarget = (GameManager.instance.players[0].transform.position - transform.position);
        // Create an angle to target 
        float Angle = Vector3.Angle(vectorToTarget, transform.forward);

        // false
        if (Angle > data.fieldOfView)
        {
            return false;
        }

        // false
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, vectorToTarget, out hitInfo, data.viewDistance);
        if (hitInfo.collider == null)
        {
            return false;
        }

        // false
        Collider targetCollider = GameManager.instance.players[0].GetComponent<Collider>();
        if (targetCollider != hitInfo.collider)
        {
            return false;
        }

        //true
        return true;
    }

    // Return true/false for noise 
    public bool hearTarget()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
        if (distance >= (GameManager.instance.players[0].noiseLevel + data.hearingDistance))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void obstacleAvoidanceMove()
    {
        // if nothing is in the way move forward
        if (canMove())
        {
            // if floor is detected move forward
            if (floorExists())
            {
                motor.move(Vector3.forward * data.movementSpeed * Time.deltaTime);
            }
            else
            {
                motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
        }
    }
    // Sets waypoint for AI
    public void getNextWaypoint()
    {
        int maxWaypoints = waypoints.Count - 1;
        if (currentWaypoint < maxWaypoints)
        {
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
        }
    }
}