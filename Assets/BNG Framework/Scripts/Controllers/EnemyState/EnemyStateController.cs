using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Action(State state);
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
        }
        get { return currentState; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentState = State.IDLE;
        enemyStates = GetComponents<IEnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (IEnemyState state in enemyStates)
        {
            state.Action(currentState);
        }
    }
}
