using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieIdle : MonoBehaviour, IEnemySubState
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

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Idle"))
        {
            animator.Play("Zombie Idle", 0);
        }
    }
}
