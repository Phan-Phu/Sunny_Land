using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartLevel : MonoBehaviour
{
    [SerializeField] int timeToWait = 5;

    private void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }
}
