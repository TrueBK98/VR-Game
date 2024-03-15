using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IDataPersistence
{
    public static SerializableDictionary<string, bool> ammoPickedUp = new SerializableDictionary<string, bool>();

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool isPickedUp = false;

    public bool IsPickedUP 
    {
        set 
        {
            isPickedUp = value;
            if (Ammo.ammoPickedUp.ContainsKey(id))
            {
                Ammo.ammoPickedUp.Remove(id);
            }
            Ammo.ammoPickedUp.Add(id, isPickedUp);
        }
        get 
        { 
            return isPickedUp;
        }
    }

    public void LoadData(GameData data)
    {
        GlobalVar.ammoPickedUp = GameData.Instance.ammoPickedUp;
        GlobalVar.ammoPickedUp.TryGetValue(id, out isPickedUp);
        try
        {
            if (isPickedUp && gameObject != null)
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

        GlobalVar.ammoPickedUp.TryGetValue(id, out isPickedUp);
        try
        {
            if (isPickedUp && gameObject != null)
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
        if (GlobalVar.ammoPickedUp.ContainsKey(id))
        {
            GlobalVar.ammoPickedUp.Remove(id);
        }
        GlobalVar.ammoPickedUp.Add(id, isPickedUp);
        GameData.Instance.ammoPickedUp = GlobalVar.ammoPickedUp;
    }
}
