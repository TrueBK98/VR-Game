using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItems : MonoBehaviour, IDataPersistence
{
    public static SerializableDictionary<string, bool> healingPickedUp = new SerializableDictionary<string, bool>();
    [SerializeField] float healingAmount = 0;

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
            if (HealingItems.healingPickedUp.ContainsKey(id))
            {
                HealingItems.healingPickedUp.Remove(id);
            }
            HealingItems.healingPickedUp.Add(id, isPickedUp);
        }
        get
        {
            return isPickedUp;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "PlayerController")
        {
            HPController hpController = collision.transform.GetComponent<HPController>();
            if (hpController)
            {
                if (hpController.CurrentHP == hpController.MaxHP)
                {
                    return;
                }

                if ((hpController.CurrentHP + healingAmount) > hpController.MaxHP)
                {
                    hpController.CurrentHP = hpController.MaxHP;
                }
                else
                {
                    hpController.CurrentHP += healingAmount;
                }

                try
                {
                    transform.GetComponent<Grabbable>().DropItem(collision.transform.GetComponent<Grabbable>().HeldByGrabbers[0]);
                }
                catch (System.Exception)
                {
                }
                IsPickedUP = true;
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void LoadData(GameData data)
    {
        GlobalVar.healingPickedUp = GameData.Instance.healingPickedUp;
        GlobalVar.healingPickedUp.TryGetValue(id, out isPickedUp);
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

        GlobalVar.healingPickedUp.TryGetValue(id, out isPickedUp);
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
        if (GlobalVar.healingPickedUp.ContainsKey(id))
        {
            GlobalVar.healingPickedUp.Remove(id);
        }
        GlobalVar.healingPickedUp.Add(id, isPickedUp);
        GameData.Instance.healingPickedUp = GlobalVar.healingPickedUp;
    }
}
