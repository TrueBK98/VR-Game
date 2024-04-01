using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour, IEnemySubState
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Action(SubState state)
    {
        if (state != SubState.ATTACK)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
        {
            animator.Play("Zombie Attack", 0);
        }
    }
}
