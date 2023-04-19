using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryList", menuName = "Scriptable Objects/Story/Story List")]
public class SO_Story : ScriptableObject
{
    public List<Story> storyList;
}