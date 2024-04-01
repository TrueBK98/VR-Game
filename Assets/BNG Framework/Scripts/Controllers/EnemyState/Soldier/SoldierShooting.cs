using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierShooting : MonoBehaviour, IEnemySubState
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Action(SubState state)
    {
        if (state != SubState.SHOOTING)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Firing Rifle"))
        {
            animator.Play("Firing Rifle", 0);
        }
    }
}
