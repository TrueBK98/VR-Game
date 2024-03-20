using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySubState
{
    void Action(SubState state);
}

public interface IEnemyState
{
    public SubState CurrentState { set; get; }
    void AddSubStates(State state);
    void RemoveSubStates(State state);
}
public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    State currentState;
    IEnemyState[] enemyStates;

    public State CurrentState
    {
        set
        {
            currentState = value;
            foreach (IEnemyState state in enemyStates)
            {
                state.RemoveSubStates(currentState);
            }

            foreach (IEnemyState state in enemyStates)
            {
                state.AddSubStates(currentState);
            }
        }
        get { return currentState; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        enemyStates = GetComponents<IEnemyState>();
        CurrentState = State.IDLE;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
