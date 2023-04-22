using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "Scriptable Object/Story/Story List")]
public class SO_Story : ScriptableObject
{
    public List<Story> storyList;
}