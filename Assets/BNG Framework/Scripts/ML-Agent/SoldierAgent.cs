using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters; 

public class SoldierAgent : Agent
{
    [SerializeField]
    Transform[] spawnPoints;

    EnemySoldier controller;
    EnemyStateController stateController;
    Damageable damageable;
    Animator animator;
    SoldierCombatState state;

    [SerializeField] Transform MuzzlePointTransform, enemyTransform, headTransform;
    [SerializeField] Damageable enemyDamageable;

    float maxHealth = 0;

    private void Start()
    {
        maxHealth = damageable.Health;
        stateController.CurrentState = State.COMBAT;
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
        sensor.AddObservation(enemyTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        /*if (actionZ > 0)
        {
            state.CurrentState = SubState.RUN_FOWARD;
        }
        else if (actionZ < 0)
        {
            state.CurrentState = SubState.RUN_BACKWARD;
        }
        else if (actionX > 0)
        {
            state.CurrentState = SubState.STRAFE_RIGHT;
        }
        else if (actionX < 0)
        {
            state.CurrentState = SubState.STRAFE_LEFT;
        }
        else
        {
            state.CurrentState = SubState.IDLE;
        }

        controller.Move(actionX, actionZ);*/

        float actionY = Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);

        stateController.ChangeAngle(transform.eulerAngles.y + (actionY * 100));

        var shoot = actionBuffers.DiscreteActions[0];
        if (shoot == 1)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }

        if (Vector3.Distance(transform.position, enemyTransform.position) <= 9.679839f)
        {
            AddReward(0.05f);
        }

        RaycastHit hit;
        if (Physics.Raycast(headTransform.position, headTransform.forward, out hit, 15f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                AddReward(0.1f);
            }
            else
            {
                AddReward(-0.01f);
            }
        }

        if (damageable.Health <= 0)
        {
            AddReward(-1f);
            EndEpisode();
        }

        if (enemyDamageable.Health <= 0)
        {
            AddReward(5f);
            EndEpisode();
        }
    }
    public override void OnEpisodeBegin()
    {
        transform.position = spawnPoints[Random.Range(0, spawnPoints.Count())].position;
        damageable.Health = maxHealth;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetMouseButtonDown(0) ? 1 : 0;
    }

    public void OnGettingHurt()
    {
        AddReward(-0.1f);
    }
}
