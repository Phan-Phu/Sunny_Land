using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTutorial : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private float TimeToResetText = 3f;

    private BoxCollider2D myBody;
    private NotifySceneManager notifySceneManager;

    private void Awake()
    {
        myBody = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        notifySceneManager = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotifySceneManager>();

        string nameScene = SceneManager.GetActiveScene().name;
        if (nameScene == "Level 1")
        {
            string text = "Press A (Left Arrow) or D (Right Arrow) to move";
            notifySceneManager.ShowTutorial(text);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            notifySceneManager.ShowTutorial(text);
        }
    }
}
