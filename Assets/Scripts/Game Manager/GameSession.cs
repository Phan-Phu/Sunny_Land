using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject levelPause;
    [SerializeField] float timeToWaitLoadScene = 2f;
    [SerializeField] private FadeLoadScene fadeLoadScene;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private GameObject UIGameplay;
    [SerializeField] private int limitMaxScore = 1500;
    [SerializeField] private NotifySceneManager notifySceneManager;


    public UnityEvent<int> updateScore;
    public UnityEvent<int> updateLives;
    public List<string> ListGemItem = new List<string>();
    public List<string> ListCherryItem = new List<string>();
    public List<string> ListEnemy = new List<string>();

    private int score;
    private int playerLives = 4;
    private bool isActiveLevelPause = false;

    public bool IsSavePos { get; set; }
    public int StartScene { get; set; }

    void Start()
    {
        StartScene = SceneManager.GetActiveScene().buildIndex;
        levelPause.SetActive(false);
        skipButton.SetActive(false);
        UIGameplay.SetActive(true);
        // update start live
        updateLives?.Invoke(playerLives);
        fadeLoadScene.FadeOutScene();
    }

    public void AddToScore(int score)
    {
        this.score += score;
        if(this.score == limitMaxScore)
        {
            AddLives(1);
            this.score = 0;
        }
        updateScore?.Invoke(this.score);
    }
    public void AddLives(int live)
    {
        playerLives += live;
        updateLives?.Invoke(playerLives);
    }

    public void LoadNextLevel()
    {
        RestartBoard();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        fadeLoadScene.FadeInScene();
        StartCoroutine(SaveLoadScene(currentScene, timeToWaitLoadScene));
    }

    private IEnumerator SaveLoadScene(int currentScene, float timeToWait)
    {
        SaveDataNextLevel(currentScene);

        yield return new WaitForSeconds(timeToWait);
        LoadLevel(currentScene);

        if (StartScene == currentScene)
        {
            ClearDataItem();
            ClearDataEnemy();
            StartScene = currentScene + 1;
        }
    }

    private void RestartBoard()
    {
        skipButton.SetActive(false);
        UIGameplay.SetActive(true);
    }

    public void ShowSkipStory(string nameScene)
    {
        string nameStory = nameScene.Substring(0, 5);
        bool checkStory = nameStory == "Story" ? true : false;
        skipButton.SetActive(checkStory);
        UIGameplay.SetActive(!checkStory);
    }

    public void SkipStory()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(SaveLoadScene(currentScene, 0));

        //set UI to default
        skipButton.SetActive(false);
        UIGameplay.SetActive(true);

        notifySceneManager.HideStory();
    }

    public void LoadLevel(int currentScene)
    {
        Time.timeScale = 1;
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
        fadeLoadScene.FadeInScene();
        yield return SceneManager.UnloadSceneAsync(currentScene);
    }

    private IEnumerator LoadSceneAsyncAndActive(int currentScene)
    {
        yield return SceneManager.LoadSceneAsync(currentScene + 1, LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneByBuildIndex(currentScene + 1);
        SceneManager.SetActiveScene(scene);
        fadeLoadScene.FadeOutScene();
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
        fadeLoadScene.FadeInScene();
        yield return new WaitForSeconds(timeToWaitLoadScene);
        if (index == 1)
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
        fadeLoadScene.FadeOutScene();
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
        fadeLoadScene.FadeInScene();
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
