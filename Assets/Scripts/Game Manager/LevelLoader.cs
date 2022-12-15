using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public void NewGame()
    {
        SaveSystem.DeleteDataLevel();
        SceneManager.LoadScene("Level 1");
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings Screen");
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 1) { return; }
        var btnContinue = GameObject.Find("Continue Button");
        if (!btnContinue) { return; }
        if (SaveSystem.LoadDataLevel() == null)
        {
            btnContinue.SetActive(false);
        }
        else
        {
            btnContinue.SetActive(true);
        }
    }
}
