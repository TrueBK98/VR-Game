using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public interface IEnemySubState
{
    void Action(SubState state);
}

public interface IEnemyState
{
    public SubState CurrentState { set; get; }
    void AddSubStates(State state);
    void RemoveSubStates(State state);
    void DisableSelf(State state);
    void EnableSelf(State state);
}
public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    State currentState;
    IEnemyState[] enemyStates;

    protected float target;
    protected float r;

    [SerializeField]
    EnemyStateController[] nearbyAllyEnemies;
    [SerializeField] float detectNearbyDistance, viewRadius, viewAngle;
    [SerializeField] LayerMask layerPlayer, layerObstacle;

    [SerializeField] Transform headTransform;
    float dirNum;

    public State CurrentState
    {
        set
        {
            currentState = value;
            foreach (IEnemyState state in enemyStates)
            {
                state.RemoveSubStates(currentState);
                state.DisableSelf(currentState);
            }

            foreach (IEnemyState state in enemyStates)
            {
                state.AddSubStates(currentState);
                state.EnableSelf(currentState);
            }
        }
        get { return currentState; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        enemyStates = GetComponents<IEnemyState>();
        CurrentState = State.IDLE;
        target = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.IDLE)
        {
            float distanceToPlayer = Vector3.Distance(Player.Instance.transform.position, transform.position);
            Vector3 playerTarget = (Player.Instance.transform.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, playerTarget);
            //Debug.Log(distanceToPlayer);

            if (distanceToPlayer <= detectNearbyDistance)
            {
                dirNum = AngleDir(transform.forward, playerTarget, transform.up);
                if (dirNum != 0)
                {
                    ChangeAngle(transform.eulerAngles.y + angleToPlayer * dirNum);
                }
                else
                {
                    ChangeAngle(transform.eulerAngles.y + angleToPlayer);
                }
                CurrentState = State.COMBAT;
                if (nearbyAllyEnemies != null && nearbyAllyEnemies.Any())
                {
                    foreach (EnemyStateController enemy in nearbyAllyEnemies)
                    {
                        enemy.CurrentState = State.COMBAT;
                    }
                }
            }
            else if (distanceToPlayer < viewRadius)
            {
                if (angleToPlayer < viewAngle / 2)
                {
                    if (Physics.Raycast(headTransform.position, playerTarget, distanceToPlayer, layerObstacle) == false)
                    {
                        dirNum = AngleDir(transform.forward, playerTarget, transform.up);
                        if (dirNum != 0)
                        {
                           ChangeAngle(transform.eulerAngles.y + angleToPlayer * dirNum); 
                        }
                        else
                        {
                            ChangeAngle(transform.eulerAngles.y + angleToPlayer);
                        }
                        CurrentState = State.COMBAT;
                        if (nearbyAllyEnemies != null && nearbyAllyEnemies.Any())
                        {
                            foreach (EnemyStateController enemy in nearbyAllyEnemies)
                            {
                                enemy.CurrentState = State.COMBAT;
                            }
                        }
                    }
                }
            }
        }

        if (currentState == State.COMBAT && name.Contains("Soldier"))
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target, ref r, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public void ChangeAngle(float targetAngle)
    {
        target = targetAngle;
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    public void AlertedOnGettingHurt()
    {
        CurrentState = State.COMBAT;
        if (nearbyAllyEnemies != null && nearbyAllyEnemies.Any())
        {
            foreach (EnemyStateController enemy in nearbyAllyEnemies)
            {
                enemy.CurrentState = State.COMBAT;
            }
        }
    }
}
