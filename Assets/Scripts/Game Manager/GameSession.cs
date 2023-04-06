using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject levelPause;

    public UnityEvent<int> updateScore;
    public UnityEvent<int> updateLives;

    public List<string> ListGemItem = new List<string>();
    public List<string> ListCherryItem = new List<string>();
    public List<string> ListEnemy = new List<string>();

    private int score;
    private int playerLives = 3;
    private bool isActiveLevelPause = false;
    public bool IsSavePos { get; set; }
    public int StartScene { get; set; }

    void Start()
    {
        StartScene = SceneManager.GetActiveScene().buildIndex;
        levelPause.SetActive(false);
    }
    public void AddToScore(int score)
    {
        this.score += score;
        updateScore?.Invoke(this.score);
    }
    public void AddLives(int live)
    {
        playerLives += live;
        updateLives?.Invoke(playerLives);
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(SaveLoadScene(currentScene));

        if (StartScene == currentScene)
        {
            ClearDataItem();
            ClearDataEnemy();
            StartScene = currentScene + 1;
        }
    }

    private IEnumerator SaveLoadScene(int currentScene)
    {
        SaveDataNextLevel(currentScene);
        yield return new WaitForSeconds(2f);
        StartCoroutine(UnLoadScene(currentScene));
        StartCoroutine(LoadSceneAsyncAndActive(currentScene));
    }

    private void SaveDataNextLevel(int currentScene)
    {
        string sceneSave = SceneUtility.GetScenePathByBuildIndex(currentScene + 1);
        string nameScene = sceneSave.Substring(sceneSave.LastIndexOf('/') + 1);
        nameScene = nameScene.Substring(0, nameScene.Length - 6);
        List<LevelData> levelDataList = SaveLoadSystem.Instance.GetSaveSystem().LoadDataLevel();
        foreach (LevelData item in levelDataList)
        {
            if (item.nameLevel == nameScene)
            {
                item.isOpen = true;
            }
        }
        ISaveSystem saveSystem = SaveLoadSystem.Instance.GetSaveSystem();
        saveSystem.SaveDataLevel(levelDataList);
    }

    private IEnumerator UnLoadScene(int currentScene)
    {
        yield return SceneManager.UnloadSceneAsync(currentScene);
    }

    private IEnumerator LoadSceneAsyncAndActive(int currentScene)
    {
        yield return SceneManager.LoadSceneAsync(currentScene + 1, LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneByBuildIndex(currentScene + 1);
        SceneManager.SetActiveScene(scene);
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGameSession();
        }
    }
    private void TakeLive()
    {
        playerLives--;
        updateLives?.Invoke(playerLives);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(DelayLoadScene(currentIndex));
    }

    private IEnumerator DelayLoadScene(int index)
    {
        yield return new WaitForSeconds(2f);
        if(index == 1)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            yield return SceneManager.UnloadSceneAsync(index);
            yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            Scene scene = SceneManager.GetSceneByBuildIndex(index);
            SceneManager.SetActiveScene(scene);
        }
    }

    private void ResetGameSession()
    {
        StartCoroutine(DelayLoadScene(1));
    }

    private void Update()
    {
        PauseLevel();
    }
    public void PauseLevel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActiveLevelPause = !isActiveLevelPause;
            levelPause.SetActive(isActiveLevelPause);
            Time.timeScale = isActiveLevelPause ? 0 : 1;
            FindObjectOfType<OptionController>().SaveAndExitPauseGame();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void ClearDataItem()
    {
        ListCherryItem.Clear();
        ListGemItem.Clear();
    }

    public void ClearDataEnemy()
    {
        ListEnemy.Clear();
    }
}
