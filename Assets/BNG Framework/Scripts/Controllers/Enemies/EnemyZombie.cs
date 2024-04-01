using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : EnemyController
{
    [SerializeField] float MaxRange, damage;
    int layerPlayer;
    EnemySoundController soundController;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        layerPlayer = ~LayerMask.NameToLayer("Player");
        soundController = GetComponent<EnemySoundController>();
    }

    public void Attack()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxRange, ~LayerMask.GetMask(), QueryTriggerInteraction.Ignore))
        {
            Damageable d = hit.collider.GetComponent<Damageable>();
            if (d)
            {
                soundController.PlayMeleeHits();
                d.DealDamage(damage, hit.point, hit.normal, true, gameObject, hit.collider.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
    }

    public override void GetHurt()
    {
        animator.Play("Zombie Reaction Hit", 1);
    }
}
