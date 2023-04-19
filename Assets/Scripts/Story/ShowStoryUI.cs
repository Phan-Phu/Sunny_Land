using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStoryUI : MonoBehaviour
{
    [SerializeField] private SO_Story storyListSO;
    [SerializeField] private string textTitle = "Introduction";
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
                storyText.text = story.textStory;
            }
        }
    }
}
