using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string nameLevel;
    public bool isOpen;

    public LevelData(string nameLevel, bool isOpen)
    {
        this.nameLevel = nameLevel;
        this.isOpen = isOpen;
    }

    public override string ToString()
    {
        return "name " + nameLevel + ": " + isOpen;
    }
}
