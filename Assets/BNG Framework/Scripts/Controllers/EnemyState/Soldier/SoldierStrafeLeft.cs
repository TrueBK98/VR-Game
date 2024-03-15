using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStrafeLeft : MonoBehaviour, IEnemyState
{
    Animator animator;
    public void Action(State state)
    {
        if (state != State.STRAFE_LEFT)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Left"))
        {
            animator.Play("Strafe Left");
        }

        GetComponent<EnemyController>().Move(0, -1);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
