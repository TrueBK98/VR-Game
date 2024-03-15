using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRunFowards : MonoBehaviour, IEnemyState
{
    Animator animator;
    public void Action(State state)
    {
        if (state != State.RUN_FOWARD)
        {
            return;
        }


        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Rifle Run"))
        {
            animator.Play("Rifle Run");
        }

        GetComponent<EnemyController>().Move(1, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
