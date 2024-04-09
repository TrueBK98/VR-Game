using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : SoldierCombatState
{
    EnemySoldier controller;
    [SerializeField]
    Transform enemyTransform;
    NavMeshAgent agent;
    EnemyStateController stateController;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemySoldier>();
        agent = GetComponent<NavMeshAgent>();
        stateController = GetComponent<EnemyStateController>();
    }

    int count = 0;
    private void FixedUpdate()
    {
        count++;

        if (count >= 20)
        {
            count = 0;

            int randomMovement = Random.Range(0, 5);

            if (Vector3.Distance(transform.position, enemyTransform.position) > 10)
            {
                agent.destination = enemyTransform.position;
                CurrentState = SubState.RUN_FOWARD;
            }
            else
            {
                agent.ResetPath();

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
            }
        }
    }
}
