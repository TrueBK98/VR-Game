using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCombatState : MonoBehaviour, IEnemyState
{
    [SerializeField]
    SubState currentState;
    IEnemySubState[] enemySubStates;

    NavMeshAgent agent;
    float speed;

    Animator animator;

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
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
    }

    public void AddSubStates(State state)
    {
        if (state != State.COMBAT)
        {
            return;
        }

        gameObject.AddComponent<ZombieChase>();
        gameObject.AddComponent<ZombieAttack>();
        gameObject.AddComponent<ZombieDie>();

        enemySubStates = GetComponents<IEnemySubState>();
        CurrentState = SubState.CHASE;
    }

    public void DisableSelf(State state)
    {
        if (state == State.COMBAT)
        {
            return;
        }
        this.enabled = false;
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            
        }
        //agent.enabled = false;
    }

    public void EnableSelf(State state)
    {
        if (state != State.COMBAT)
        {
            return;
        }

        this.enabled = true;
        //agent.enabled = true;
        agent.destination = Player.Instance.transform.position;
    }

    public void RemoveSubStates(State state)
    {
        if (state == State.COMBAT)
        {
            return;
        }

        Destroy(GetComponent<ZombieChase>());
        Destroy(GetComponent<ZombieAttack>());
        Destroy(GetComponent<ZombieDie>());
    }

    private void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    CurrentState = SubState.ATTACK;
                    agent.speed = 0f;
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    CurrentState = SubState.CHASE;
                    agent.speed = speed;
                }
            }
        }

        agent.destination = Player.Instance.transform.position;
    }

    public void onDie()
    {
        CurrentState = SubState.DIE;
        enabled = false;
        agent.enabled = false;
        GetComponent<ZombieSoundController>().enabled = false;
    }
}
