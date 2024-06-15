using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : EnemyController
{
    [SerializeField] float MaxRange, damage;
    int layerPlayer;
    EnemySoundController soundController;
    [SerializeField] Transform hitTransform;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        layerPlayer = LayerMask.NameToLayer("Player");
        soundController = GetComponent<EnemySoundController>();
    }

    public void Attack()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(hitTransform.position, Player.Instance.transform.position - hitTransform.position, out hit, MaxRange, ~layerPlayer, QueryTriggerInteraction.Ignore))
        {
            HPController d = hit.collider.GetComponent<HPController>();
            if (d)
            {
                soundController.PlayMeleeHits();
                d.DealDamage(damage, hit.point, hit.normal, true, gameObject, hit.collider.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(hitTransform.position, hitTransform.forward, Color.green);
    }

    public override void GetHurt()
    {
        animator.Play("Zombie Reaction Hit", 1);
    }
}
