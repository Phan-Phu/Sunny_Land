using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    [Tooltip("Use in Menu Level")]
    [SerializeField] private List<LoadSceneLevel> loadSceneLevelList = new List<LoadSceneLevel>();

    private void Start()
    {
        foreach (LoadSceneLevel item in loadSceneLevelList)
        {
            item.onLoadLevel += (object sender, string nameScene) =>
            {
                LoadLevel(nameScene);
            };
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Menu Level");
    }
    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu Screen");
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings Screen");
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void LoadLevel(string nameScene)
    {
        if(nameScene == "") { return; }
        if(nameScene == "Level 1")
        {
            LoadStoryScene();
        }
        else
        {
            LoadNextLevel(nameScene);
        }
    }

    private void LoadStoryScene()
    {
        string nameScene = "Story Screen";
        SceneManager.LoadScene(nameScene);
    }

    public void LoadNextLevel(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        SceneManager.LoadSceneAsync("Persistent Scene", LoadSceneMode.Additive);
    }
}
