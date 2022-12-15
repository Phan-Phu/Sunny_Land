using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    private int level;
    public LevelData(int level)
    {
        this.level = level;
    }
    public int GetLevel()
    {
        return level;
    }
}
