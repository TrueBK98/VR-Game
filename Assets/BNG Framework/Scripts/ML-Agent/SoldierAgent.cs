using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.AI;

public class SoldierAgent : Agent
{
    [SerializeField]
    Transform[] Spawnpoints;

    EnemySoldier controller;
    EnemyStateController stateController;
    Damageable damageable;
    Animator animator;
    SoldierCombatState state;

    [SerializeField] Transform MuzzlePointTransform, enemyTransform;
    [SerializeField] Damageable enemyDamageable;

    float maxHealth = 0;

    private void Start()
    {
        maxHealth = damageable.Health;
    }

    public override void Initialize()
    {
        controller = GetComponent<EnemySoldier>();
        damageable = GetComponent<Damageable>();
        animator = GetComponent<Animator>();
        state = GetComponent<SoldierCombatState>();
        stateController = GetComponent<EnemyStateController>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(MuzzlePointTransform.position);
        sensor.AddObservation(MuzzlePointTransform.rotation);
        sensor.AddObservation(MuzzlePointTransform.forward);
        sensor.AddObservation(enemyTransform.position);
        sensor.AddObservation(enemyDamageable.Health);
        sensor.AddObservation(damageable.Health);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        switch (actionBuffers.DiscreteActions[1])
        {
            case 0:
                state.CurrentState = SubState.IDLE;
                break;
            case 1:
                state.CurrentState = SubState.RUN_FOWARD;
                controller.Move(1, 0);
                break;
            case 2:
                state.CurrentState = SubState.RUN_BACKWARD;
                controller.Move(-1, 0);
                break;
            default:
                break;
        }

        float actionY = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);

        stateController.ChangeAngle(actionY * 180);
        
        var shoot = actionBuffers.DiscreteActions[0];
        if (shoot == 1)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }

        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

        if (distanceToEnemy >= 9.679839f)
        {
            AddReward(-0.01f);
        }
        else if (distanceToEnemy > 4) 
        {
            AddReward(0.01f);
        }
        else
        {
            AddReward(-0.01f);
        }

        if (enemyDamageable.Health <= 0)
        {
            AddReward(1f);
            EndEpisode();
        }

        if (damageable.Health <= 0)
        {
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        //transform.position = Spawnpoints[UnityEngine.Random.Range(0, Spawnpoints.Count())].position;
        /*transform.position = new Vector3(UnityEngine.Random.Range(Spawnpoints[0].position.x, Spawnpoints[1].position.x), transform.position.y, UnityEngine.Random.Range(Spawnpoints[0].position.z, Spawnpoints[1].position.z));
        GetComponent<Rigidbody>().isKinematic = false;
        damageable.Health = maxHealth;
        damageable.destroyed = false;*/
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetMouseButtonDown(0) ? 1 : 0;
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[1] = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[1] = 2;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            discreteActionsOut[1] = 0;
        }
    }

    public void OnGettingHurt()
    {
        //AddReward(-0.1f);
    }

    /*private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-0.1f);
        }
    }*/
}
