using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float timeToDelay = 3f;
    [SerializeField] [Range(0f, 1f)] float levelExitSlowFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string nameCurrentScene = SceneManager.GetActiveScene().name;
        TextTitle textComplete = GetComponent<TextTitle>();
        textComplete.SetImage();
        GetComponent<TextTitle>().ShowTitle(nameCurrentScene + " Complete");
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowFactor;
        yield return new WaitForSeconds(timeToDelay);
        Time.timeScale = 1f;
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.IsSavePos = false;

        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
            
        if (gameSession.OriginScene == currentScene)
        {
            gameSession.ListGemItem.Clear();
            gameSession.ListCherryItem.Clear();
            gameSession.ListEnemy.Clear();
            gameSession.OriginScene = currentScene + 1;
        }
        Destroy(gameObject);

        //SaveSystem.SaveDataLevel();
    }
}
