using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BNG;
using UnityEngine.SceneManagement;

public class HPController : Damageable, IDataPersistence
{
    public GameObject HealthBar;
    public GameObject Locomotion;

    public float MaxHP
    {
        set
        {
            _startingHealth = value;
            Health = _startingHealth;
        }
        get { return _startingHealth; }
    }

    public float CurrentHP
    {
        set
        {
            Health = value;
            if (Health < 0) Health = 0;
            DisplayValue();
        }
        get
        {
            return Health;
        }
    }

    public override void DealDamage(float damageAmount, Vector3? hitPosition = null, Vector3? hitNormal = null, bool reactToHit = true, GameObject sender = null, GameObject receiver = null)
    {
        if (destroyed)
        {
            return;
        }

        CurrentHP -= damageAmount;

        onDamaged?.Invoke(damageAmount);

        // Invector Integration
#if INVECTOR_BASIC || INVECTOR_AI_TEMPLATE
            if(SendDamageToInvector) {
                var d = new Invector.vDamage();
                d.hitReaction = reactToHit;
                d.hitPosition = (Vector3)hitPosition;
                d.receiver = receiver == null ? this.gameObject.transform : null;
                d.damageValue = (int)damageAmount;

                this.gameObject.ApplyDamage(new Invector.vDamage(d));
            }
#endif

        if (Health <= 0)
        {
            DestroyThis();
            GetComponent<LocomotionManager>().enabled = false;
            GetComponent<SmoothLocomotion>().enabled = false;
            GetComponent<PlayerTeleport>().enabled = false;
            Locomotion.SetActive(false);
        }
    }

    public new void LoadData(GameData data)
    {
        GlobalVar.hpAtSpawnpoint = GameData.Instance.currentHP;
    }

    public new void SaveData(GameData data)
    {
        GlobalVar.hpAtSpawnpoint = CurrentHP;
        GameData.Instance.currentHP = GlobalVar.hpAtSpawnpoint;
    }

    protected void DisplayValue()
    {
        HealthBar.transform.localScale = new Vector3((float)(Health / _startingHealth), transform.localScale.y, transform.localScale.z);
    }

    public void RespawnPlayer()
    {
        if (GlobalVar.spawnPoint == Vector3.zero)
        {
            GlobalVar.weapons = new WeaponType[3];
        }
        damageableDestroyed = GlobalVar.damageableDestroyed;
        RaycastWeapon.weaponsPickedUp = GlobalVar.weaponsPickedUp;
        Ammo.ammoPickedUp = GlobalVar.ammoPickedUp;
        EnemyTriggerZone.enemyTriggerEnabled = GlobalVar.enemyTriggerEnabled;
        HealingItems.healingPickedUp = GlobalVar.healingPickedUp;
        SceneManager.LoadScene(1);
    }
}
