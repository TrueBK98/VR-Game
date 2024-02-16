using BNG;
using LTA.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    public int pistolAmmo = 0;
    public int rifleAmmo = 0;
    public int shotgunAmmo = 0;
    public bool pickUpAmmo = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!pickUpAmmo) { return;}

        bool heldAmmo = false;
        if (collision.transform.name.Contains("PistolClip") && collision.transform.parent == null)
        {
            pistolAmmo += collision.gameObject.GetComponentsInChildren<Bullet>(false).Length;
            heldAmmo = true;
        }

        if (collision.transform.name.Contains("RifleClip") && collision.transform.parent == null)
        {
            rifleAmmo += collision.gameObject.GetComponentsInChildren<Bullet>(false).Length;
            heldAmmo = true;
        }

        if (collision.transform.name.Contains("Shell") && !collision.transform.name.Contains("ShellEmpty") && collision.transform.parent == null)
        {
            shotgunAmmo++;
            heldAmmo = true;
        }

        if (heldAmmo)
        {
            collision.transform.GetComponent<Grabbable>().DropItem(collision.transform.GetComponent<Grabbable>().HeldByGrabbers[0]);
            GameObject.Destroy(collision.gameObject);
        }
    }
}

public class AmmoInven : SingletonMonoBehaviour<AmmoInventory>
{

}