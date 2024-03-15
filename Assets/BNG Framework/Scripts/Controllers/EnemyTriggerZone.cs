using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerZone : CheckTriggerController, IDataPersistence
{
    public static SerializableDictionary<string, bool> enemyTriggerEnabled = new SerializableDictionary<string, bool>();
    [SerializeField]
    EnemyController[] enemyControllers;
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        OnTriggerAction = EnableEnemies;
        foreach (EnemyController enemy in enemyControllers)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    protected void EnableEnemies()
    {
        triggered = true;
        foreach (EnemyController enemy in enemyControllers)
        {
            enemy.gameObject.SetActive(true);
        }
        if (EnemyTriggerZone.enemyTriggerEnabled.ContainsKey(id))
        {
            EnemyTriggerZone.enemyTriggerEnabled.Remove(id);
        }
        EnemyTriggerZone.enemyTriggerEnabled.Add(id, triggered);
        gameObject.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        GlobalVar.enemyTriggerEnabled = GameData.Instance.enemyTriggerEnabled;
        GlobalVar.enemyTriggerEnabled.TryGetValue(id, out triggered);
        try
        {
            if (triggered && gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
        catch (System.Exception)
        {
        }
    }

    public void LoadLastCheckpoint()
    {
        if (id == null)
        {
            return;
        }

        GlobalVar.enemyTriggerEnabled.TryGetValue(id, out triggered);
        try
        {
            if (triggered && gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
        catch (System.Exception)
        {
        }
    }

    public void SaveData(GameData data)
    {
        if (GlobalVar.enemyTriggerEnabled.ContainsKey(id))
        {
            GlobalVar.enemyTriggerEnabled.Remove(id);
        }
        GlobalVar.enemyTriggerEnabled.Add(id, triggered);
        GameData.Instance.enemyTriggerEnabled = GlobalVar.enemyTriggerEnabled;
    }
}
