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

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void NewGame()
    {
        GlobalVar.currentLevel = 0;
        GlobalVar.spawnPoint = Vector3.zero;
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        GlobalVar.currentLevel = DataManager.Instance.levelVO.GetCurrentLevel();
        GlobalVar.spawnPoint = DataManager.Instance.levelVO.GetSpawnPoint();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        PauseMenu.SetActive(false);
        ConfirmSave.SetActive(true);
    }

    public void ConfirmSaveGame()
    {
        DataManager.Instance.levelVO.SaveCurrentProgress();
        ConfirmSave.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void CancelSaveGame()
    {
        ConfirmSave.SetActive(false);
        PauseMenu.SetActive(true);
    }
}
