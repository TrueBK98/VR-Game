using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SoldierIdleState : MonoBehaviour, IEnemyState
{
    [SerializeField]
    SubState currentState;
    IEnemySubState[] enemySubStates;

    NavMeshAgent agent;

    [SerializeField] Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int currentPatrolPointIndex;
    float speed;

    public SubState CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            foreach (IEnemySubState subState in enemySubStates)
            {
                subState.Action(currentState);
            }
        }
    }

    public void AddSubStates(State state)
    {
        if (state != State.IDLE)
        {
            return;
        }

        gameObject.AddComponent<SoldierIdle>();
        gameObject.AddComponent<SoldierRunFowards>();
        gameObject.AddComponent<SoldierDie>();

        enemySubStates = GetComponents<IEnemySubState>();
        CurrentState = SubState.IDLE;
    }

    public void RemoveSubStates(State state)
    {
        if (state == State.IDLE)
        {
            return;
        }

        Destroy(GetComponent<SoldierIdle>());
        Destroy(GetComponent<SoldierRunFowards>());
        Destroy(GetComponent<SoldierDie>());
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        currentPatrolPointIndex = 0;
        if (patrolPoints.Any())
        {
            currentPatrolPoint = patrolPoints[currentPatrolPointIndex];
            agent.destination = currentPatrolPoint.position;
        }
    }

    public int restTimer = 300;
    private void Update()
    {
        if (patrolPoints.Any())
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        restTimer = 0;
                        if ((currentPatrolPointIndex + 1) == patrolPoints.Length)
                        {
                            currentPatrolPointIndex = 0;
                        }
                        else
                        {
                            currentPatrolPointIndex++;
                        }

                        currentPatrolPoint = patrolPoints[currentPatrolPointIndex];
                    }
                }
            }

            if (restTimer < 300)
            {
                CurrentState = SubState.IDLE;
                agent.speed = 0;
                restTimer++;
            }
            else
            {
                CurrentState = SubState.RUN_FOWARD;
                agent.speed = speed;
            }

            agent.destination = currentPatrolPoint.position;
        }
    }

    public void DisableSelf(State state)
    {
        if (state == State.IDLE)
        {
            return;
        }
        this.enabled = false;
        agent.enabled = false;
    }

    public void EnableSelf(State state)
    {
        if (state != State.IDLE)
        {
            return;
        }

        this.enabled = true;
    }

    public void onDie()
    {
        CurrentState = SubState.DIE;
        agent.enabled = false;
    }
}
