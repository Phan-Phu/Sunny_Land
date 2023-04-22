using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowStory : MonoBehaviour
{
    [SerializeField] SO_Story storySO;
    private NotifySceneManager notifySceneManager;

    private void Start()
    {
        notifySceneManager = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotifySceneManager>();
        string nameScene = SceneManager.GetActiveScene().name;
        notifySceneManager.ShowTitle(nameScene);
    }

    public void ShowingStory(string title)
    {
        if(notifySceneManager == null) { return; }
        foreach (Story story in storySO.storyList)
        {
            if(story.titleText == title)
            {
                notifySceneManager.ShowStory(story.textStory);
            }
        }
    }
}
