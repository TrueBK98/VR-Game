using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject ConfirmSave;

    private void Start()
    {
        DataManager.Instance.LoadData();    
    }

    public void OnQuitGameClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnNewGameClicked()
    {
        GlobalVar.currentLevel = 0;
        GlobalVar.spawnPoint = Vector3.zero;
        GlobalVar.hpAtSpawnpoint = 100;
        GlobalVar.pistolAmmoAtSpawnpoint = 0;
        GlobalVar.rifleAmmoAtSpawnpoint = 0;
        GlobalVar.shotgunAmmoAtSpawnpoint = 0;
        GlobalVar.weapons = new WeaponType[3];
        GlobalVar.pistolLoadedShot = false;
        GlobalVar.rifleLoadedShot = false;
        GlobalVar.shotgunLoadedShot = false;
        GlobalVar.pistolBulletCount = 0;
        GlobalVar.shotgunBulletCount = 0;
        GlobalVar.rifleBulletCount = 0;
        GlobalVar.damageableDestroyed = new SerializableDictionary<string, bool>();
        GlobalVar.weaponsPickedUp = new SerializableDictionary<string, bool>();
        GlobalVar.ammoPickedUp = new SerializableDictionary<string, bool>();
        GlobalVar.enemyTriggerEnabled = new SerializableDictionary<string, bool>();
        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadScene(1);
    }

    public void OnLoadGameClicked()
    {
        Time.timeScale = 1;
        MapController.clickedLoad = true;
        DataPersistenceManager.Instance.LoadGame();
        SceneManager.LoadScene(1);
    }

    public void OnBackToMainMenuClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnSaveGameClicked()
    {
        if (EnemyController.CheckAnyActiveEnemy())
        {
            return;
        }
        PauseMenu.SetActive(false);
        ConfirmSave.SetActive(true);
    }

    public void OnConfirmSaveGameClicked()
    {
        DataPersistenceManager.Instance.SaveGame();
        ConfirmSave.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void OnCancelSaveGameClicked()
    {
        ConfirmSave.SetActive(false);
        PauseMenu.SetActive(true);
    }
}
