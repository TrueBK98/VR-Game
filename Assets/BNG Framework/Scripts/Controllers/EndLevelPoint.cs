using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPoint : CheckTriggerController
{
    private void Start()
    {
        OnTriggerAction = NextLevel;
    }
    protected void NextLevel()
    {
        GlobalVar.currentLevel++;
        GlobalVar.spawnPoint = Vector3.zero;
        SceneManager.LoadScene(1);
    }
}
