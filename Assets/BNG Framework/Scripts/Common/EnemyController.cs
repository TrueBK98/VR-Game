using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;



public class EnemyController : MoveController
{
    protected Animator animator;
    
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        speed = 2.0f;
    }

    
    // Update is called once per frame
    
    int count2 = 0;
    int count3 = 0;
    protected void Update()
    {



        /*if (count2 > 2000)
        {
            isShooting = false;
        }
        else
        {
            isShooting = true;
            count2++;
        }*/
    }

    public static bool CheckAnyActiveEnemy()
    {
        foreach (EnemyController enemy in FindObjectsOfType<EnemyController>())
        {
            if (enemy.isActiveAndEnabled)
            {
                return true;
            }
        }
        return false;
    }

    public virtual void GetHurt()
    {
        animator.Play("Hit Reaction", 2);
    }
}

public enum State
{
    IDLE,
    COMBAT
}

public enum SubState
{
    IDLE,
    RUN_FOWARD,
    RUN_BACKWARD,
    STRAFE_LEFT,
    STRAFE_RIGHT
}