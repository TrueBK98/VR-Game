using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Action(State state);
}

public class EnemyController : MoveController
{
    [SerializeField]
    State currentState;
    IEnemyState[] enemyStates;
    void Start()
    {
        currentState = State.IDLE;
        speed = 2.0f;
        enemyStates = GetComponents<IEnemyState>();
    }

    public State CurrentState
    { 
        set 
        {
            currentState = value;
        }
        get { return currentState; } 
    }
    // Update is called once per frame
    int count  = 0;
    void Update()
    {
        /*if (count > 300)
        {
            if (currentState == State.IDLE)
            {
                CurrentState = State.RUN_FOWARD;
            }
            else if (currentState == State.RUN_FOWARD)
            {
                CurrentState = State.RUN_BACKWARD;
            }
            else if (currentState == State.RUN_BACKWARD)
            {
                CurrentState = State.STRAFE_RIGHT;
            }
            else if (currentState == State.STRAFE_RIGHT)
            {
                CurrentState = State.STRAFE_LEFT;
            }
            else if (currentState == State.STRAFE_LEFT)
            {
                CurrentState = State.IDLE;
            }
            count = 0;
        }
        else
        {
            count++;
        }

        foreach (IEnemyState state in enemyStates)
        {
            state.Action(currentState);
        }*/
    }

    public static bool CheckAnyActiveEnemy()
    {
        foreach (EnemyController enemy in FindObjectsOfType<EnemyController>())
        {
            if (enemy.isActiveAndEnabled)
            {
                return true;
            }
        }
        return false;
    }
}

public enum State
{
    IDLE,
    RUN_FOWARD,
    RUN_BACKWARD,
    STRAFE_LEFT,
    STRAFE_RIGHT
}