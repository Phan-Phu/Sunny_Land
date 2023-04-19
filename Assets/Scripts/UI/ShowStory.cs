using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowStory : MonoBehaviour
{
    private NotifySceneManager notifySceneManager;

    private void Start()
    {
        notifySceneManager = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotifySceneManager>();
        string nameScene = SceneManager.GetActiveScene().name;
        notifySceneManager.ShowTitle(nameScene);
    }

    public void ShowingStory(string text)
    {
        if(notifySceneManager == null) { return; }
        notifySceneManager.ShowStory(text);
    }
}
