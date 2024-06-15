using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoldierCombatState : MonoBehaviour, IEnemyState
{
    [SerializeField]
    SubState currentState;
    IEnemySubState[] enemySubStates;
    protected Animator animator;
    SoldierAgent agent;

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

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<SoldierAgent>();
    }

    public void AddSubStates(State state)
    {
        if (state != State.COMBAT)
        {
            return;
        }

        gameObject.AddComponent<SoldierIdle>();
        gameObject.AddComponent<SoldierRunBackwards>();
        gameObject.AddComponent<SoldierRunFowards>();
        gameObject.AddComponent<SoldierStrafeLeft>();
        gameObject.AddComponent<SoldierStrafeRight>();
        gameObject.AddComponent<SoldierShooting>();
        gameObject.AddComponent<SoldierDie>();

        enemySubStates = GetComponents<IEnemySubState>();
        CurrentState = SubState.IDLE;
    }

    public void RemoveSubStates(State state)
    {
        if (state == State.COMBAT)
        {
            return;
        }

        Destroy(GetComponent<SoldierIdle>());
        Destroy(GetComponent<SoldierRunBackwards>());
        Destroy(GetComponent<SoldierRunFowards>());
        Destroy(GetComponent<SoldierStrafeLeft>());
        Destroy(GetComponent<SoldierStrafeRight>());
        Destroy(GetComponent<SoldierShooting>());
        Destroy(GetComponent<SoldierDie>());
    }

    public void DisableSelf(State state)
    {
        if (state == State.COMBAT)
        {
            return;
        }
        this.enabled = false;
    }

    public void EnableSelf(State state)
    {
        if (state != State.COMBAT)
        {
            return;
        }
        this.enabled = true;
        agent.enabled = true;
    }

    protected void Update()
    {
        if (currentState == SubState.IDLE && animator.GetBool("isShooting") && !animator.GetCurrentAnimatorStateInfo(1).IsName("Reloading"))
        {
            CurrentState = SubState.SHOOTING;
        }

        if (currentState == SubState.SHOOTING && !animator.GetBool("isShooting"))
        {
            CurrentState = SubState.IDLE;
        }

        if (currentState == SubState.SHOOTING && animator.GetCurrentAnimatorStateInfo(1).IsName("Reloading"))
        {
            CurrentState = SubState.IDLE;
        }
    }

    public void onDie()
    {
        CurrentState = SubState.DIE;
        agent.enabled = false;
        animator.SetBool("isShooting", false);
    }
}
