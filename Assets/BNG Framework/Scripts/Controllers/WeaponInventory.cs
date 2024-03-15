using BNG;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using LTA.DesignPattern;
using Unity.VisualScripting;
using System.Linq;
using TMPro;

public class WeaponInventory : MonoBehaviour, IDataPersistence
{
    public WeaponType[] weapons;
    public WeaponType currentWeapon = WeaponType.BareHand;
    public GameObject weaponSelectorPrefab;
    public List<GameObject> pooledWeapons = new List<GameObject>();
    public Transform PlayerController;
    GameObject weaponSelector = null;
    public GameObject activeWeapon = null;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject rifle;
    public GameObject pistolClip;
    public GameObject rifleClip;
    public Grabber grabber;
    public TextMeshProUGUI ammoText;

    public WeaponType CurrentWeapon
    {
        get { return currentWeapon; }
        set 
        {
            if (currentWeapon == value) { return;}
            currentWeapon = value;
            StartCoroutine(couroutine());
            /*if (activeWeapon != null)
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
            }*/
        }
    }

    IEnumerator couroutine()
    {
        yield return new WaitForEndOfFrame();
        if (activeWeapon != null)
        {
            activeWeapon.GetComponent<Grabbable>().DropItem(grabber);
            if (pooledWeapons.Contains(activeWeapon))
            {
                activeWeapon.SetActive(false);
            }
            else
            {
                GameObject.Destroy(activeWeapon);
            }
        }

        bool ExistedInPool = false;
        if (pooledWeapons.Any())
        {
            foreach (GameObject weapon in pooledWeapons)
            {
                if (weapon.GetComponent<RaycastWeapon>().type == currentWeapon)
                {
                    activeWeapon = weapon;
                    activeWeapon.SetActive(true);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    ExistedInPool = true;
                    break;
                }
            }

        }

        if (!ExistedInPool)
        {
            switch (currentWeapon)
            {
                case WeaponType.BareHand:
                    break;
                case WeaponType.Pistol:
                    activeWeapon = Instantiate(pistol, grabber.transform.position, grabber.transform.rotation);
                    activeWeapon.GetComponent<Grabbable>().CanBeDropped = false;
                    pooledWeapons.Add(activeWeapon);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    activeWeapon.GetComponent<RaycastWeapon>().BulletInChamber = GlobalVar.pistolLoadedShot;
                    if (GlobalVar.pistolBulletCount > 0)
                    {
                        GameObject newPistolClip = Instantiate(pistolClip, activeWeapon.GetNamedChild("ClipInsert").transform.position, activeWeapon.GetNamedChild("ClipInsert").transform.rotation);
                        newPistolClip.transform.SetParent(activeWeapon.GetNamedChild("ClipInsert").transform);
                        Bullet[] ammoInClip = newPistolClip.GetComponentsInChildren<Bullet>(false);
                        for (int i = 0; i < ammoInClip.Length - GlobalVar.pistolBulletCount; i++)
                        {
                            GameObject.Destroy(ammoInClip[i]);
                        }
                    }
                    break;
                case WeaponType.Shotgun:
                    activeWeapon = Instantiate(shotgun, grabber.transform.position, grabber.transform.rotation);
                    activeWeapon.GetComponent<Grabbable>().CanBeDropped = false;
                    pooledWeapons.Add(activeWeapon);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    activeWeapon.GetComponent<RaycastWeapon>().BulletInChamber = GlobalVar.shotgunLoadedShot;
                    if (GlobalVar.shotgunBulletCount > 0)
                    {
                        for (int i = 0; i < GlobalVar.shotgunBulletCount; i++)
                        {
                            GameObject b = new GameObject();
                            b.AddComponent<Bullet>();
                            b.transform.parent = activeWeapon.transform;
                        }
                    }
                    break;
                case WeaponType.Rifle:
                    activeWeapon = Instantiate(rifle, grabber.transform.position, grabber.transform.rotation);
                    activeWeapon.GetComponent<Grabbable>().CanBeDropped = false;
                    pooledWeapons.Add(activeWeapon);
                    grabber.GrabGrabbable(activeWeapon.GetComponent<Grabbable>());
                    activeWeapon.GetComponent<RaycastWeapon>().BulletInChamber = GlobalVar.rifleLoadedShot;
                    if (GlobalVar.rifleBulletCount > 0)
                    {
                        GameObject newRifleClip = Instantiate(rifleClip, activeWeapon.GetNamedChild("ClipInsert").transform.position, activeWeapon.GetNamedChild("ClipInsert").transform.rotation);
                        newRifleClip.transform.SetParent(activeWeapon.GetNamedChild("ClipInsert").transform);
                        Bullet[] ammoInClip = newRifleClip.GetComponentsInChildren<Bullet>(false);
                        for (int i = 0; i < ammoInClip.Length - GlobalVar.rifleBulletCount; i++)
                        {
                            GameObject.Destroy(ammoInClip[i]);
                        }
                    }
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

    private void Awake()
    {
        weapons = new WeaponType[3];
    }


    // Update is called once per frame
    void Update()
    {
        if (activeWeapon != null)
        {
            switch (currentWeapon)
            {
                case WeaponType.BareHand:
                    ammoText.text = "0/0";
                    break;
                case WeaponType.Pistol:
                    int pistolLoadedShot = activeWeapon.GetComponent<RaycastWeapon>().BulletInChamber ? 1 : 0;
                    ammoText.text = (activeWeapon.GetComponent<RaycastWeapon>().GetBulletCount() + pistolLoadedShot).ToString() + "/" + AmmoInven.Instance.pistolAmmo.ToString();
                    break;
                case WeaponType.Shotgun:
                    int shotgunLoadedShot = activeWeapon.GetComponent<RaycastWeapon>().BulletInChamber ? 1 : 0;
                    ammoText.text = (activeWeapon.GetComponent<RaycastWeapon>().GetBulletCount() + shotgunLoadedShot).ToString() + "/" + AmmoInven.Instance.shotgunAmmo.ToString();
                    break;
                case WeaponType.Rifle:
                    int rifleLoadedShot = activeWeapon.GetComponent<RaycastWeapon>().BulletInChamber ? 1 : 0;
                    ammoText.text = (activeWeapon.GetComponent<RaycastWeapon>().GetBulletCount() + rifleLoadedShot).ToString() + "/" + AmmoInven.Instance.rifleAmmo.ToString();
                    break;
                default:
                    break;
            }
        }

        if (ToggleHandsInput.GetDown() && !Player.Instance.GetComponent<HPController>().destroyed)
        {
            grabber.GetComponent<SphereCollider>().isTrigger = false;
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
                            weaponSelector.transform.Find("Shotgun").gameObject.SetActive(true);
                            break;
                        case WeaponType.Rifle:
                            weaponSelector.transform.Find("Rifle").gameObject.SetActive(true);
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
            grabber.GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    public void LoadData(GameData data)
    {
        GlobalVar.weapons = GameData.Instance.weapons;
        GlobalVar.pistolLoadedShot = GameData.Instance.pistolLoadedShot;
        GlobalVar.rifleLoadedShot = GameData.Instance.rifleLoadedShot;
        GlobalVar.shotgunLoadedShot = GameData.Instance.shotgunLoadedShot;
        GlobalVar.pistolBulletCount = GameData.Instance.pistolBulletCount;
        GlobalVar.shotgunBulletCount = GameData.Instance.shotgunBulletCount;
        GlobalVar.rifleBulletCount = GameData.Instance.rifleBulletCount;
    }

    public void SaveData(GameData data)
    {
        GlobalVar.weapons = weapons;
        GameData.Instance.weapons = GlobalVar.weapons;
        foreach (GameObject weapon in pooledWeapons)
        {
            switch (weapon.GetComponent<RaycastWeapon>().type)
            {
                case WeaponType.Pistol:
                    GlobalVar.pistolLoadedShot = weapon.GetComponent<RaycastWeapon>().BulletInChamber;
                    GameData.Instance.pistolLoadedShot = GlobalVar.pistolLoadedShot;
                    GlobalVar.pistolBulletCount = weapon.GetComponent<RaycastWeapon>().GetBulletCount();
                    GameData.Instance.pistolBulletCount = GlobalVar.pistolBulletCount;
                    break;
                case WeaponType.Shotgun:
                    GlobalVar.shotgunLoadedShot = weapon.GetComponent<RaycastWeapon>().BulletInChamber;
                    GameData.Instance.shotgunLoadedShot = GlobalVar.shotgunLoadedShot;
                    GlobalVar.shotgunBulletCount = weapon.GetComponent<RaycastWeapon>().GetBulletCount();
                    GameData.Instance.shotgunBulletCount = GlobalVar.shotgunBulletCount;
                    break;
                case WeaponType.Rifle:
                    GlobalVar.rifleLoadedShot = weapon.GetComponent<RaycastWeapon>().BulletInChamber;
                    GameData.Instance.rifleLoadedShot = GlobalVar.rifleLoadedShot;
                    GlobalVar.rifleBulletCount = weapon.GetComponent<RaycastWeapon>().GetBulletCount();
                    GameData.Instance.rifleBulletCount = GlobalVar.rifleBulletCount;
                    break;
                default:
                    break;
            }
        }
    }
}

public enum WeaponType
{
    BareHand = 0,
    Pistol = 1,
    Shotgun = 2,
    Rifle= 3,
}

public class WeaponInven : SingletonMonoBehaviour<WeaponInventory>
{

}