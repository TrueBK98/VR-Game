using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoints : CheckTriggerController
{
    private void Start()
    {
        OnTriggerAction = SetPlayerSpawnpoint;
    }
    protected void SetPlayerSpawnpoint()
    {
        if (EnemyController.CheckAnyActiveEnemy())
        {
            return;
        }
        GlobalVar.spawnPoint = Player.Instance.transform.position;
        GlobalVar.hpAtSpawnpoint = Player.Instance.GetComponent<HPController>().CurrentHP;
        GlobalVar.pistolAmmoAtSpawnpoint = AmmoInven.Instance.pistolAmmo;
        GlobalVar.rifleAmmoAtSpawnpoint = AmmoInven.Instance.rifleAmmo;
        GlobalVar.shotgunAmmoAtSpawnpoint = AmmoInven.Instance.shotgunAmmo;
        GlobalVar.weapons = WeaponInven.Instance.weapons;
        foreach (GameObject weapon in WeaponInven.Instance.pooledWeapons)
        {
            switch (weapon.GetComponent<RaycastWeapon>().type)
            {
                case WeaponType.Pistol:
                    GlobalVar.pistolLoadedShot = weapon.GetComponent<RaycastWeapon>().BulletInChamber;
                    GlobalVar.pistolBulletCount = weapon.GetComponent<RaycastWeapon>().GetBulletCount();
                    break;
                case WeaponType.Shotgun:
                    GlobalVar.shotgunLoadedShot = weapon.GetComponent<RaycastWeapon>().BulletInChamber;
                    GlobalVar.shotgunBulletCount = weapon.GetComponent<RaycastWeapon>().GetBulletCount();
                    break;
                case WeaponType.Rifle:
                    GlobalVar.rifleLoadedShot = weapon.GetComponent<RaycastWeapon>().BulletInChamber;
                    GlobalVar.rifleBulletCount = weapon.GetComponent<RaycastWeapon>().GetBulletCount();
                    break;
                default:
                    break;
            }
        }
        GlobalVar.damageableDestroyed = Damageable.damageableDestroyed;
        GlobalVar.weaponsPickedUp = RaycastWeapon.weaponsPickedUp;
        GlobalVar.ammoPickedUp = Ammo.ammoPickedUp;
        GlobalVar.enemyTriggerEnabled = EnemyTriggerZone.enemyTriggerEnabled;
        GlobalVar.healingPickedUp = HealingItems.healingPickedUp;
    }
}
