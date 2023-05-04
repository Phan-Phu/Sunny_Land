using UnityEngine;

[System.Serializable]
public class Story
{
    public string titleText = "title text ";
    [TextArea(10, 20)]
    public string textStory = "story text";
}
