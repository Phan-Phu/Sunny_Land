using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NotifySceneManager : MonoBehaviour
{
    [SerializeField] private Image frameTutorial;
    [SerializeField] private TextMeshProUGUI textTutorial;

    [SerializeField] private Image frameTitle;
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] Sprite shortSprite;
    [SerializeField] Sprite longSprite;

    [SerializeField] private float timeToReset = 2f;

    private void Awake()
    {
        frameTutorial.enabled = false;
    }

    public void ShowTutorial(string text)
    {
        StartCoroutine(ShowTextTutorial(text));
    }

    public void ShowTitle(string title)
    {
        StartCoroutine(ShowTextTitle(title));
    }

    private IEnumerator ShowTextTutorial(string text)
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