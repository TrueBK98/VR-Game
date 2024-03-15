using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRunBackwards : MonoBehaviour, IEnemyState
{
    Animator animator;
    public void Action(State state)
    {
        if (state != State.RUN_BACKWARD)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run Backwards"))
        {
            animator.Play("Run Backwards");
        }

        GetComponent<EnemyController>().Move(-1, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}