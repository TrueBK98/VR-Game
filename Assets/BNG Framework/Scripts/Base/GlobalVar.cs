using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar
{
    public static int currentLevel = 0;
    public static Vector3 spawnPoint;
    public static float hpAtSpawnpoint;
    public static int pistolAmmoAtSpawnpoint;
    public static int rifleAmmoAtSpawnpoint;
    public static int shotgunAmmoAtSpawnpoint;
    public static WeaponType[] weapons;
    public static bool pistolLoadedShot;
    public static bool rifleLoadedShot;
    public static bool shotgunLoadedShot;
    public static int pistolBulletCount;
    public static int rifleBulletCount;
    public static int shotgunBulletCount;
    public static SerializableDictionary<string, bool> damageableDestroyed;
    public static SerializableDictionary<string, bool> weaponsPickedUp;
    public static SerializableDictionary<string, bool> ammoPickedUp;
    public static SerializableDictionary<string, bool> enemyTriggerEnabled;
}
