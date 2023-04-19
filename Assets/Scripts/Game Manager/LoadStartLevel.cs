using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartLevel : MonoBehaviour
{
    [SerializeField] int timeToWait = 5;

    private void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime(currentSceneIndex));
        }
    }
    IEnumerator WaitForTime(int currentSceneIndex)
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
