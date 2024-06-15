using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    public List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
    }

    public void LoadGame()
    {
        GameData gameData = dataHandler.Load();
        if (gameData != null)
        {
            GameData.Instance.currentLevel = gameData.currentLevel;
            GameData.Instance.playerPosition = gameData.playerPosition;
            GameData.Instance.currentHP = gameData.currentHP;
            GameData.Instance.pistolAmmo = gameData.pistolAmmo;
            GameData.Instance.rifleAmmo = gameData.rifleAmmo;
            GameData.Instance.shotgunAmmo = gameData.shotgunAmmo;
            GameData.Instance.weapons = gameData.weapons;
            GameData.Instance.pistolLoadedShot = gameData.pistolLoadedShot;
            GameData.Instance.rifleLoadedShot = gameData.rifleLoadedShot;
            GameData.Instance.shotgunLoadedShot = gameData.shotgunLoadedShot;
            GameData.Instance.pistolBulletCount = gameData.pistolBulletCount;
            GameData.Instance.shotgunBulletCount = gameData.shotgunBulletCount;
            GameData.Instance.rifleBulletCount = gameData.rifleBulletCount;
            GameData.Instance.damageableDestroyed = gameData.damageableDestroyed;
            GameData.Instance.weaponsPickedUp = gameData.weaponsPickedUp;
            GameData.Instance.ammoPickedUp = gameData.ammoPickedUp;
            GameData.Instance.enemyTriggerEnabled = gameData.enemyTriggerEnabled;
            GameData.Instance.healingPickedUp = gameData.healingPickedUp;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData.Instance);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(GameData.Instance);
        }

        dataHandler.Save(GameData.Instance);
    }
}
