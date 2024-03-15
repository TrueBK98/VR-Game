using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStrafeRight : MonoBehaviour, IEnemyState
{
    Animator animator;
    public void Action(State state)
    {
        if (state != State.STRAFE_RIGHT)
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Right"))
        {
            animator.Play("Strafe Right");
        }

        GetComponent<EnemyController>().Move(0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
