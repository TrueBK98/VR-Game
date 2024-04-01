using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChase : MonoBehaviour, IEnemySubState
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Action(SubState state)
    {
        if (state != SubState.CHASE)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Run"))
        {
            animator.Play("Zombie Run", 0);
        }
    }
}
