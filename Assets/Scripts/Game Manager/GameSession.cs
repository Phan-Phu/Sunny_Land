using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    private static GameSession instance;

    [SerializeField] GameObject levelPause;

    SavePoint savePoint;
    public static GameSession Instance => instance;
    public UnityEvent<int> updateScore;
    public UnityEvent<int> updateLives;

    public List<string> ListGemItem = new List<string>();
    public List<string> ListCherryItem = new List<string>();
    public List<string> ListEnemy = new List<string>();

    private int score;
    private int playerLives = 3;
    public bool IsSavePos { get; set; }
    public bool isAlive { get; set; }
    public int OriginScene;

    private void Awake()
    {
        instance = this;
        OriginScene = SceneManager.GetActiveScene().buildIndex;
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
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

        savePoint = FindObjectOfType<SavePoint>();
        if (!savePoint) { return; }
        IsSavePos = savePoint.boolTouch;
    }
    private IEnumerator DelayLoadScene(int index)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
        if(index == 0)
            Destroy(gameObject);
    }


    private void ResetGameSession()
    {
        StartCoroutine(DelayLoadScene(0));
    }


    private void Update()
    {
        PauseLevel();
        //LoadContinueLevel();
        CheckCurrentScene();
    }
    private void LoadContinueLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == 2)
        {
            LevelData data = SaveSystem.LoadDataLevel();
            if (data == null) { return; }
            int conLevel = data.GetLevel();
            SceneManager.LoadScene("Level " + conLevel);
        }
    }
    public void PauseLevel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && levelPause.activeSelf == false)
        {
            levelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && levelPause.activeSelf == true)
        {
            levelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    private void CheckCurrentScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == 4)
        {
            Destroy(gameObject);
        }
    }
}
