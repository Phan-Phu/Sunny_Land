using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelExit : MonoBehaviour
{
    private NotifySceneManager notifySceneManager;
    private string nameScene;
    int currentScene = 0;

    private void Start()
    {
        nameScene = SceneManager.GetActiveScene().name;
        notifySceneManager = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotifySceneManager>();
        notifySceneManager.ShowTitle(nameScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowLevelComplete();
        int index = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != index)
        {
            currentScene = index;
            GameSession gameSession = GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>();
            gameSession.LoadNextLevel();
        }
    }

    private void ShowLevelComplete()
    {
        notifySceneManager.ShowTitle(nameScene + " completed");
    }
}
