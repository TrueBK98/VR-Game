using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerController : MonoBehaviour
{
    protected Action OnTriggerAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerController")
        {
            if (OnTriggerAction != null)
            {
                OnTriggerAction();
            }
        }
    }
}
