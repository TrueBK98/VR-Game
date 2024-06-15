using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDie : MonoBehaviour, IEnemySubState
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Action(SubState state)
    {
        if (state != SubState.DIE)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Die"))
        {
            animator.Play("Zombie Die", 0);
        }
    }
}
