using BNG;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using LTA.DesignPattern;
using Unity.VisualScripting;

public class WeaponInventory : MonoBehaviour
{
    public WeaponType[] weapons;
    public WeaponType currentWeapon = WeaponType.BareHand;
    public GameObject weaponSelectorPrefab;
    public Transform PlayerController;
    GameObject weaponSelector = null;
    public GameObject activeWeapon = null;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject knife;
    public GameObject rifle;
    public Grabber grabber;

    public WeaponType CurrentWeapon
    {
        get { return currentWeapon; }
        set 
        {
            if (currentWeapon == value) { return;}
            if (activeWeapon != null)
            {
                activeWeapon.GetComponent<Grabbable>().DropItem(grabber);
                GameObject.Destroy(activeWeapon);
            }

            currentWeapon = value;

            switch (currentWeapon)
            {
                case WeaponType.BareHand:
                    break;
                case WeaponType.Pistol:
                    activeWeapon = Instantiate(pistol, grabber.transform.position, grabber.transform.rotation);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    break;
                case WeaponType.Shotgun:
                    activeWeapon = Instantiate(shotgun, grabber.transform.position, grabber.transform.rotation);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    break;
                case WeaponType.Knife:
                    activeWeapon = Instantiate(knife, grabber.transform.position, grabber.transform.rotation);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    break;
                case WeaponType.Rifle:
                    activeWeapon = Instantiate(rifle, grabber.transform.position, grabber.transform.rotation);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    break;
                default:
                    break;
            }
        }
    }

    float xDistance;
    float yDistance;
    float zDistance;

    [Tooltip("Input used to open the weapon selector")]
    public ControllerBinding ToggleHandsInput = ControllerBinding.RightThumbstickDown;
    // Start is called before the first frame update
    void Start()
    {
        weapons = new WeaponType[4];
    }

    // Update is called once per frame
    void Update()
    {        
        if (ToggleHandsInput.GetDown())
        {
            if (weaponSelector == null)
            {
                weaponSelector = Instantiate(weaponSelectorPrefab, transform.position, transform.rotation);
                xDistance = weaponSelector.transform.position.x - PlayerController.position.x;
                yDistance = weaponSelector.transform.position.y - PlayerController.position.y;
                zDistance = weaponSelector.transform.position.z - PlayerController.position.z;
                foreach (WeaponType weapon in weapons)
                {
                    if (weapon == 0)
                    {
                        continue;
                    }

                    switch (weapon)
                    {
                        case WeaponType.Pistol:
                            weaponSelector.transform.Find("Pistol").gameObject.SetActive(true);
                            break;
                        case WeaponType.Shotgun:
                            weaponSelector.transform.Find("Pistol").gameObject.SetActive(true);
                            break;
                        case WeaponType.Knife:
                            weaponSelector.transform.Find("Pistol").gameObject.SetActive(true);
                            break;
                        case WeaponType.Rifle:
                            weaponSelector.transform.Find("Pistol").gameObject.SetActive(true);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                weaponSelector.transform.position = new Vector3(PlayerController.position.x + xDistance, PlayerController.position.y + yDistance, PlayerController.position.z + zDistance);
            }
        }
        else if (!ToggleHandsInput.GetDown())
        {
            Destroy(weaponSelector);
        }
    }
}

public enum WeaponType
{
    BareHand = 0,
    Pistol = 1,
    Shotgun = 2,
    Knife = 3,
    Rifle= 4,
}

public class WeaponInven : SingletonMonoBehaviour<WeaponInventory>
{

}