using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class NotifySceneManager : MonoBehaviour
{
    [SerializeField] private Image frameTutorial;
    [SerializeField] private TextMeshProUGUI textTutorial;

    [SerializeField] private Image frameTitle;
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] Sprite shortSprite;
    [SerializeField] Sprite longSprite;

    [SerializeField] private float timeToReset = 2f;
    [SerializeField] private float timeToResetStory = 2f;

    private void Awake()
    {
        frameTutorial.enabled = false;
    }

    private void Start()
    {
        //listener event form scene
        GameSession gameSession = GetComponentInParent<GameSession>();
    }

    public void ShowTutorial(string text)
    {
        StartCoroutine(ShowTextTutorial(text, timeToReset));
    }

    public void ShowStory(string text)
    {
        ShowTextStory(text);
    }

    public void HideStory()
    {
        HideTextStory();
    }

    public void ShowTitle(string title)
    {
        StartCoroutine(ShowTextTitle(title));
    }

    private void ShowTextStory(string text)
    {
        frameTutorial.enabled = true;
        textTutorial.text = text.ToString();
    }

    private void HideTextStory()
    {
        textTutorial.text = "";
        frameTutorial.enabled = false;
    }


    private IEnumerator ShowTextTutorial(string text, float timeToReset)
    {
        frameTutorial.enabled = true;
        textTutorial.text = text.ToString();
        yield return new WaitForSeconds(timeToReset);
        textTutorial.text = "";
        frameTutorial.enabled = false;
    }

    private IEnumerator ShowTextTitle(string name)
    {
        int limitCharacterImage = 10;
        if (name.Length > limitCharacterImage)
        {
            frameTitle.sprite = longSprite;
        }
        else
        {
            frameTitle.sprite = shortSprite;
        }
        frameTitle.SetNativeSize();
        frameTitle.enabled = true;
        textTitle.text = name;
        yield return new WaitForSeconds(timeToReset);
        textTitle.text = "";
        frameTitle.enabled = false;
    }
}
