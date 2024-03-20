using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRunBackwards : MonoBehaviour, IEnemySubState
{
    Animator animator;
    public void Action(SubState state)
    {
        if (state != SubState.RUN_BACKWARD)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run Backwards"))
        {
            animator.Play("Run Backwards", 0);
        }

        //GetComponent<EnemyController>().Move(-1, 0);
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
