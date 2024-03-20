using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SoldierCombatState : MonoBehaviour, IEnemyState
{
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
    }
}
