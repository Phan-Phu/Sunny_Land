using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStoryUI : MonoBehaviour
{
    [SerializeField] private SO_Story storyListSO;
    [SerializeField] private string textTitle = "Introduction";
    [Tooltip("Audio can be null")]
    [SerializeField] private AudioClip typingAudio;
    private TextMeshProUGUI storyText;


    void Start()
    {
        storyText = GetComponent<TextMeshProUGUI>();
        Show(textTitle);
    }

    public void Show(string textTitle)
    {
        foreach (Story story in storyListSO.storyList)
        {
            if (story.titleText == textTitle)
            {
                //storyText.text = story.textStory;
                StartCoroutine(AnimationParagraph(story.textStory));
            }
        }
    }

    private IEnumerator AnimationParagraph(string paragraph)
    {
        char[] characters = paragraph.ToCharArray();
        for (int i = 0; i < characters.Length; i++)
        {
            storyText.text += characters[i].ToString();
            float timeToWait = 0.05f;
            PlayAudio();
            yield return new WaitForSeconds(timeToWait);
        }
    }

    private void PlayAudio()
    {
        if(typingAudio == null) { return; }
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(typingAudio, Camera.main.transform.position, volume);
    }
}
