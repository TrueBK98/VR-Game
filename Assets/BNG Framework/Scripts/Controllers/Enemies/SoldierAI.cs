using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : SoldierCombatState
{
    [SerializeField]
    Transform[] Spawnpoints;

    EnemySoldier controller;
    [SerializeField]
    Transform enemyTransform;
    NavMeshAgent agent;
    EnemyStateController stateController;

    Damageable damageable;
    [SerializeField] Damageable enemyDamageable;
    float maxHealth = 0;

    [SerializeField]
    SoldierAgent soldierAgent;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemySoldier>();
        agent = GetComponent<NavMeshAgent>();
        stateController = GetComponent<EnemyStateController>();
        damageable = GetComponent<Damageable>();
        maxHealth = damageable.Health;
    }

    int count = 0;

    new void Update()
    {
        base.Update();
        if (soldierAgent.StepCount >= soldierAgent.MaxStep - 1)
        {
            ResetAI();
        }
    }

    private void FixedUpdate()
    {
        if (damageable.Health <= 0)
        {
            ResetAI();
        }

        if (enemyDamageable.Health <= 0)
        {
            ResetAI();
        }

        count++;

        if (count >= 20)
        {
            count = 0;

            int randomMovement = Random.Range(0, 5);
            int ramdomShoot = Random.Range(0, 2);

            if (Vector3.Distance(transform.position, enemyTransform.position) > 10)
            {
                animator.SetBool("isShooting", false);
                agent.enabled = true;
                agent.destination = enemyTransform.position;
                CurrentState = SubState.RUN_FOWARD;
            }
            else
            {
                if (agent.enabled == true)
                {
                    agent.ResetPath();
                }
                agent.enabled = false;

                switch (randomMovement)
                {
                    case 0:
                        CurrentState = SubState.IDLE;
                        break;
                    case 1:
                        CurrentState = SubState.RUN_FOWARD;
                        break;
                    case 2:
                        CurrentState = SubState.RUN_BACKWARD;
                        break;
                    case 3:
                        CurrentState = SubState.STRAFE_RIGHT;
                        break;
                    case 4:
                        CurrentState = SubState.STRAFE_LEFT;
                        break;
                    default:
                        break;
                }

                stateController.ChangeAngle(transform.eulerAngles.y + Random.Range(-100, 101));

                if (ramdomShoot == 1)
                {
                    animator.SetBool("isShooting", true);
                }
                else
                {
                    animator.SetBool("isShooting", false);
                }
            }
        }

        switch (CurrentState)
        {
            case SubState.RUN_FOWARD:
                controller.Move(1, 0);
                break;
            case SubState.RUN_BACKWARD:
                controller.Move(-1, 0);
                break;
            case SubState.STRAFE_LEFT:
                controller.Move(0, 1);
                break;
            case SubState.STRAFE_RIGHT:
                controller.Move(0, -1);
                break;
            default:
                break;
        }
    }

    public void ResetAI()
    {
        transform.position = Spawnpoints[UnityEngine.Random.Range(0, Spawnpoints.Count())].position;
        GetComponent<Rigidbody>().isKinematic = false;
        damageable.Health = maxHealth;
    }
}
