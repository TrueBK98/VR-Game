using BNG;
using LTA.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour, IDataPersistence
{
    public int pistolAmmo = 0;
    public int rifleAmmo = 0;
    public int shotgunAmmo = 0;
    public bool pickUpAmmo = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!pickUpAmmo) { return;}

        bool heldAmmo = false;

        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.name.Contains("Grabber"))
            {
                return;
            }
        }

        if (collision.transform.name.Contains("PistolClip"))
        {
            pistolAmmo += collision.gameObject.GetComponentsInChildren<Bullet>(false).Length;
            heldAmmo = true;
        }

        if (collision.transform.name.Contains("RifleClip"))
        {
            rifleAmmo += collision.gameObject.GetComponentsInChildren<Bullet>(false).Length;
            heldAmmo = true;
        }

        if (collision.transform.name.Contains("Shell") && !collision.transform.name.Contains("ShellEmpty"))
        {
            shotgunAmmo++;
            heldAmmo = true;
        }

        if (heldAmmo)
        {
            collision.gameObject.GetComponent<Ammo>().IsPickedUP = true;
            collision.transform.GetComponent<Grabbable>().DropItem(collision.transform.GetComponent<Grabbable>().HeldByGrabbers[0]);
            GameObject.Destroy(collision.gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        GlobalVar.pistolAmmoAtSpawnpoint = GameData.Instance.pistolAmmo;
        GlobalVar.rifleAmmoAtSpawnpoint = GameData.Instance.rifleAmmo;
        GlobalVar.shotgunAmmoAtSpawnpoint = GameData.Instance.shotgunAmmo;
    }

    public void SaveData(GameData data)
    {
        GlobalVar.pistolAmmoAtSpawnpoint = pistolAmmo;
        GlobalVar.rifleAmmoAtSpawnpoint = rifleAmmo;
        GlobalVar.shotgunAmmoAtSpawnpoint = shotgunAmmo;
        GameData.Instance.pistolAmmo = GlobalVar.pistolAmmoAtSpawnpoint;
        GameData.Instance.rifleAmmo = GlobalVar.rifleAmmoAtSpawnpoint;
        GameData.Instance.shotgunAmmo = GlobalVar.shotgunAmmoAtSpawnpoint;
    }
}

public class AmmoInven : SingletonMonoBehaviour<AmmoInventory>
{

}