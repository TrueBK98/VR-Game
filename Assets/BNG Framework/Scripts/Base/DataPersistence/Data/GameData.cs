using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;

[System.Serializable]
public class GameData : Singleton<GameData>
{
    public int currentLevel;
    public Vector3 playerPosition;
    public float currentHP;
    public int pistolAmmo;
    public int rifleAmmo;
    public int shotgunAmmo;
    public WeaponType[] weapons;
    public bool pistolLoadedShot;
    public bool rifleLoadedShot;
    public bool shotgunLoadedShot;
    public int pistolBulletCount;
    public int rifleBulletCount;
    public int shotgunBulletCount;
    public SerializableDictionary<string, bool> damageableDestroyed;
    public SerializableDictionary<string, bool> weaponsPickedUp;
    public SerializableDictionary<string, bool> ammoPickedUp;
    public SerializableDictionary<string, bool> enemyTriggerEnabled;
    public SerializableDictionary<string, bool> healingPickedUp;
    public GameData() 
    {
        this.currentLevel = 0;
        playerPosition = Vector3.zero;
        currentHP = 100;
        pistolAmmo = 0;
        rifleAmmo = 0;
        shotgunAmmo = 0;
        weapons = new WeaponType[3];
        pistolLoadedShot = false;
        shotgunLoadedShot = false;
        rifleLoadedShot = false;
        pistolBulletCount = 0;
        rifleBulletCount = 0;
        shotgunBulletCount = 0;
        damageableDestroyed = new SerializableDictionary<string, bool>();
        weaponsPickedUp = new SerializableDictionary<string, bool>();
        ammoPickedUp = new SerializableDictionary<string, bool>();
        enemyTriggerEnabled = new SerializableDictionary<string, bool>();
        healingPickedUp = new SerializableDictionary<string, bool>();
    }
}
