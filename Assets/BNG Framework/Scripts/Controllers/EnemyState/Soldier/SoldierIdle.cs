using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdle : MonoBehaviour, IEnemySubState
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Action(SubState state)
    {
        if (state != SubState.IDLE)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Rifle Aiming Idle"))
        {
            animator.Play("Rifle Aiming Idle", 0);
        }
    }
}
