using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper_AI : MonoBehaviour
{
    Controller_AI controller;

    public float healthLastKnown;

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
        healthLastKnown = controller.data.healthCurrent;
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
        Vector3 targetPosition = (-(controller.distanceToMaintain * GameManager.instance.players[0].transform.forward));
        // If we are not far enough away
        if (Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position) <= controller.distanceToMaintain)
        {
            controller.obstacleAvoidanceMove();
            controller.motor.rotateTowards(targetPosition);
        }
        // when behind ,shoot at the player
        else
        {
            controller.motor.rotateTowards(GameManager.instance.players[0].transform.position - transform.position);
            controller.motor.ShootMissile();
        }
        // go into flee state if hit
        if (controller.data.healthCurrent < healthLastKnown)
        {
            healthLastKnown = controller.data.healthCurrent;
            currentState = states.flee;
        }
        // resume patrolling
        if (!controller.hearTarget() && !controller.canSeeTarget())
        {
            currentState = states.patrol;
        }

    }

    void stateFlee()
    {
        if (controller.timeInFlee <= controller.timeToFlee)
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
            controller.timeInFlee += Time.deltaTime;
        }
        else
        {
            controller.timeInFlee = 0;
            //back to patrol state
            currentState = states.patrol;
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
        if (controller.canSeeTarget() || controller.hearTarget())
        {
            // go into chase state
            currentState = states.chase;
        }
    }
}


