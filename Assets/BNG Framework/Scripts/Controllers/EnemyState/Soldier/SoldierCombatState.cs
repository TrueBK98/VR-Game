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
    }

    private void Update()
    {
        if (currentState == SubState.IDLE && animator.GetBool("isShooting"))
        {
            CurrentState = SubState.SHOOTING;
        }
    }
}
