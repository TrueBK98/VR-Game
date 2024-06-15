using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour, IDataPersistence
{
    public static bool clickedLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;   
        }

        MapInfo mapInfo = DataManager.Instance.mapVO.GetMapInfo(GlobalVar.currentLevel);
        GameObject prefabMap = Resources.Load<GameObject>(mapInfo.path);
        Instantiate(prefabMap);

        if (GlobalVar.spawnPoint != Vector3.zero)
        {
            Player.Instance.transform.position = GlobalVar.spawnPoint;
        }

        StartCoroutine(couroutine());
    }

    IEnumerator couroutine()
    {
        yield return new WaitForEndOfFrame();
        if (clickedLoad)
        {
            DataPersistenceManager.Instance.dataPersistenceObjects = DataPersistenceManager.Instance.FindAllDataPersistenceObjects();
            DataPersistenceManager.Instance.LoadGame();
            clickedLoad = false;
        }
        else
        {
            if (GlobalVar.spawnPoint != Vector3.zero)
            {
                if (GlobalVar.damageableDestroyed.Any())
                {
                    foreach (Damageable damageable in FindObjectsOfType<Damageable>())
                    {
                        damageable.LoadLastCheckpoint();
                    }
                }

                if (GlobalVar.weaponsPickedUp.Any())
                {
                    foreach (RaycastWeapon weapon in FindObjectsOfType<RaycastWeapon>())
                    {
                        weapon.LoadLastCheckpoint();
                    }
                }

                if (GlobalVar.ammoPickedUp.Any())
                {
                    foreach (Ammo ammo in FindObjectsOfType<Ammo>())
                    {
                        ammo.LoadLastCheckpoint();
                    }
                }

                if (GlobalVar.enemyTriggerEnabled.Any())
                {
                    foreach (EnemyTriggerZone triggerZone in FindObjectsOfType<EnemyTriggerZone>())
                    {
                        triggerZone.LoadLastCheckpoint();
                    }
                }

                if (GlobalVar.healingPickedUp.Any())
                {
                    foreach (HealingItems item in FindObjectsOfType<HealingItems>())
                    {
                        item.LoadLastCheckpoint();
                    }
                }
            }
        }

        Player.Instance.GetComponent<HPController>().CurrentHP = GlobalVar.hpAtSpawnpoint;
        AmmoInven.Instance.pistolAmmo = GlobalVar.pistolAmmoAtSpawnpoint;
        AmmoInven.Instance.rifleAmmo = GlobalVar.rifleAmmoAtSpawnpoint;
        AmmoInven.Instance.shotgunAmmo = GlobalVar.shotgunAmmoAtSpawnpoint;
        WeaponInven.Instance.weapons = GlobalVar.weapons;
    }

    public void LoadData(GameData data)
    {
        GlobalVar.currentLevel = data.currentLevel;
        GlobalVar.spawnPoint = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.currentLevel = GlobalVar.currentLevel;
        GlobalVar.spawnPoint = Player.Instance.transform.position;
        data.playerPosition = GlobalVar.spawnPoint;
    }
}
