using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySolder : EnemyController
{
    [SerializeField] Transform muzzlePointTransform;
    [SerializeField] float MaxRange, damage;
    [SerializeField] GameObject HitFXPrefab;
    [Tooltip("Play this sound on shoot")]
    public AudioClip GunShotSound;

    [Tooltip("Volume to play the GunShotSound clip at. Range 0-1")]
    [Range(0.0f, 1f)]
    public float GunShotVolume = 0.75f;

    [Tooltip("Make this active on fire. Randomize scale / rotation")]
    public GameObject MuzzleFlashObject;

    protected IEnumerator shotRoutine;
    int layerPlayer;

    [SerializeField] int maxAmmo;
    int shotFired;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        layerPlayer = ~LayerMask.NameToLayer("Player");
        shotFired = 0;
    }

    int count = 0;
    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (count > 30)
        {
            //GetComponent<Damageable>().DealDamage(1);
            if (animator.GetBool("isShooting")) { Shoot(); }

            count = 0;
        }
        else
        {
            count++;
        }
    }

    public void Shoot()
    {
        if (shotFired >= maxAmmo)
        {
            if (!animator.GetCurrentAnimatorStateInfo(1).IsName("Reloading"))
            {
                animator.Play("Reloading", 1);
            }
            return;
        }
        shotFired++;

        VRUtils.Instance.PlaySpatialClipAt(GunShotSound, muzzlePointTransform.position, GunShotVolume);

        RaycastHit hit;
        //Debug.DrawRay(muzzlePointTransform.position, muzzlePointTransform.forward, Color.green);
        if (Physics.Raycast(muzzlePointTransform.position, muzzlePointTransform.forward, out hit, MaxRange, ~LayerMask.GetMask(), QueryTriggerInteraction.Ignore))
        {
            if (HitFXPrefab)
            {
                GameObject impact = Instantiate(HitFXPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;

                // Attach bullet hole to object if possible
                BulletHole hole = impact.GetComponent<BulletHole>();
                if (hole)
                {
                    hole.TryAttachTo(hit.collider);
                }

                Destroy(impact, 1f);
            }

            Damageable d = hit.collider.GetComponent<Damageable>();
            if (d)
            {
                d.DealDamage(damage, hit.point, hit.normal, true, gameObject, hit.collider.gameObject);
            }
        }

        if (shotRoutine != null)
        {
            MuzzleFlashObject.SetActive(false);
            StopCoroutine(shotRoutine);
        }

        shotRoutine = doMuzzleFlash();
        StartCoroutine(shotRoutine);
    }

    void Reload()
    {
        shotFired = 0;
    }

    protected virtual IEnumerator doMuzzleFlash()
    {
        MuzzleFlashObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);

        randomizeMuzzleFlashScaleRotation();
        yield return new WaitForSeconds(0.05f);

        MuzzleFlashObject.SetActive(false);
    }

    void randomizeMuzzleFlashScaleRotation()
    {
        MuzzleFlashObject.transform.localScale = Vector3.one * Random.Range(0.75f, 1.5f);
        MuzzleFlashObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 90f));
    }
}
