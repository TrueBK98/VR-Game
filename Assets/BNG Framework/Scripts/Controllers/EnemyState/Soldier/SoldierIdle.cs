using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdle : MonoBehaviour, IEnemyState
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Action(State state)
    {
        if (state != State.IDLE)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Rifle Aiming Idle"))
        {
            animator.Play("Rifle Aiming Idle");
        }
    }
}
