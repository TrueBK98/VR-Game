using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStrafeRight : MonoBehaviour, IEnemySubState
{
    Animator animator;
    public void Action(SubState state)
    {
        if (state != SubState.STRAFE_RIGHT)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Right"))
        {
            animator.Play("Strafe Right", 0);
        }

        //GetComponent<EnemyController>().Move(0, 1);
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
