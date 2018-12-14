using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldsGround_AI : MonoBehaviour
{
    Controller_AI controller;

    public enum states
    {
        chase,
        flee,
        patrol
    };
    public states currentState;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<Controller_AI>();
        currentState = states.patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case states.chase:
                stateChase();
                break;
            case states.flee:
                stateFlee();
                break;
            case states.patrol:
                statePatrol();
                break;
        }
    }

    void stateChase()
    {
        // Hold ground and shoot 
        Vector3 targetLocation = GameManager.instance.players[0].transform.position - transform.position;
        // rotate to players position
        controller.motor.rotateTowards(targetLocation);
        // keep firing on cooldown
        controller.motor.ShootMissile();

        // go into flee state if
        if (controller.data.healthCurrent <= (controller.data.healthMax / 2)) 
        {
            currentState = states.flee;
        }
        //  resume patrolling
        if (!controller.hearTarget() && !controller.canSeeTarget())
        {
            currentState = states.patrol;
        }
    }

    void stateFlee()
    {
        float distanceFromTarget = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
        if (controller.distanceToMaintain >= distanceFromTarget)
        {
            // run away from player
            Vector3 directionToFlee = -(GameManager.instance.players[0].transform.position - transform.position);
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(directionToFlee);
            }
            controller.obstacleAvoidanceMove();
            // increase fleeing time
        }
        else
        {
            //back to chase state
            currentState = states.chase;
        }
    }

    void statePatrol()
    {
        // patrol between waypoints
        Vector3 targetPosition = new Vector3(controller.waypoints[controller.currentWaypoint].position.x, transform.position.y, controller.waypoints[controller.currentWaypoint].position.z);
        Vector3 dirToWaypoint = targetPosition - transform.position;
        if (controller.canMove())
        {
            controller.obstacleAvoidanceMove();
            controller.motor.rotateTowards(dirToWaypoint);
        }
        controller.obstacleAvoidanceMove();

        if (Vector3.Distance(transform.position, targetPosition) <= controller.HowClose)
        {
            controller.getNextWaypoint();
        }
        if (controller.hearTarget() || controller.canSeeTarget())
        {
            // go into chase state
            currentState = states.chase;
        }
    }
}
