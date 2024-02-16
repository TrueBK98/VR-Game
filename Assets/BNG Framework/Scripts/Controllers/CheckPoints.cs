using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : CheckTriggerController
{
    private void Start()
    {
        OnTriggerAction = SetPlayerSpawnpoint;
    }
    protected void SetPlayerSpawnpoint()
    {
        GlobalVar.spawnPoint = Player.Instance.transform.position;
    }
}
