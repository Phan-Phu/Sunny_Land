using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be update if Level Data inheritance abstract all save data in file then, Update interface to generic
public interface ISaveSystem 
{
    public void SaveDataLevel(List<LevelData> levelDataList);

    public List<LevelData> LoadDataLevel();

    public void DeleteDataLevel();
}
