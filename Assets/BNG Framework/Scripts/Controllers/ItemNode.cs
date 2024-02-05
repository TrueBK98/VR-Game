using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.BareHand;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Grabber")
        {
            if (other.transform.parent.name == "RightController")
            {
                WeaponInven.Instance.CurrentWeapon = weaponType;
            }
        }
    }
}
