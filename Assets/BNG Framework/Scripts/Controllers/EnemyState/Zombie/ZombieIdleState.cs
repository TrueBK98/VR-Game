using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieIdleState : MonoBehaviour, IEnemyState
{
    NavMeshAgent agent;
    [SerializeField]
    SubState currentState;
    IEnemySubState[] enemySubStates;
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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void AddSubStates(State state)
    {
        if (state != State.IDLE)
        {
            return;
        }

        gameObject.AddComponent<ZombieIdle>();
        gameObject.AddComponent<ZombieDie>();

        enemySubStates = GetComponents<IEnemySubState>();
        CurrentState = SubState.IDLE;
    }

    public void DisableSelf(State state)
    {
        if (state == State.IDLE)
        {
            return;
        }
        this.enabled = false;
    }

    public void EnableSelf(State state)
    {
        if (state != State.IDLE)
        {
            return;
        }

        this.enabled = true;
    }

    public void RemoveSubStates(State state)
    {
        if (state == State.IDLE)
        {
            return;
        }

        Destroy(GetComponent<ZombieIdle>());
        Destroy(GetComponent<ZombieDie>());
    }

    public void onDie()
    {
        CurrentState = SubState.DIE;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<ZombieSoundController>().enabled = false;
    }
}
