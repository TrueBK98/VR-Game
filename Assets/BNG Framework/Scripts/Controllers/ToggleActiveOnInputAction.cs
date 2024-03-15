using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BNG {

    /// <summary>
    /// This script will toggle a GameObject whenever the provided InputAction is executed
    /// </summary>
    public class ToggleActiveOnInputAction : MonoBehaviour {

        public InputActionReference InputAction = default;
        public GameObject ToggleObject = default;

        private void OnEnable() {
            InputAction.action.performed += ToggleActive;
        }

        private void OnDisable() {
            InputAction.action.performed -= ToggleActive;
        }

        public void ToggleActive(InputAction.CallbackContext context) {
            if(ToggleObject) {
                if (Player.Instance.GetComponent<HPController>().destroyed)
                {
                    return;
                }

                ToggleObject.SetActive(!ToggleObject.activeSelf);
                if (ToggleObject.activeSelf)
                {
                    Time.timeScale = 0;
                    if (WeaponInven.Instance.currentWeapon != WeaponType.BareHand)
                    {
                        WeaponInven.Instance.activeWeapon.GetComponent<Grabbable>().DropItem(WeaponInven.Instance.grabber);
                        WeaponInven.Instance.activeWeapon.SetActive(false);
                    }
                }
                else
                {
                    Time.timeScale = 1;
                    if (WeaponInven.Instance.currentWeapon != WeaponType.BareHand)
                    {
                        WeaponInven.Instance.activeWeapon.SetActive(true);
                        WeaponInven.Instance.grabber.GrabGrabbable(WeaponInven.Instance.activeWeapon.GetComponent<Grabbable>());
                    }
                }
            }
        }
    }
}

