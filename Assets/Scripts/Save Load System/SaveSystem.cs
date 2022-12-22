using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour, ISaveSystem
{
    public static SaveSystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Has more than Save Sytem " + Instance + ", " + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SaveLoadSystem.Instance.SetSaveSystem(this);
    }

    public void SaveDataLevel(List<LevelData> levelDataList)
    {
        var saveLevel = new ListLevelSave { listLevelSave = levelDataList };
        string jsonData = JsonUtility.ToJson(saveLevel);

        string path = Application.streamingAssetsPath + "/Level.json";
        File.WriteAllText(path, jsonData);
        Debug.Log("Save Data Successful!");
    }
    public List<LevelData> LoadDataLevel()
    {
        string path = Application.streamingAssetsPath + "/Level.json";
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            if(jsonData == "") { return null; }
            List<LevelData> levelDataList = new List<LevelData>();
            ListLevelSave listLevelSave = JsonUtility.FromJson<ListLevelSave>(jsonData);
            foreach (LevelData level in listLevelSave.listLevelSave)
            {
                levelDataList.Add(level);
            }
            return levelDataList;
        }
        return null;
    }
    public void DeleteDataLevel()
    {
        string path = Application.streamingAssetsPath + "/Level.json";
        if (File.Exists(path))
        {
            print("Delete file successful!");
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning("Save file is not found in: " + path);
        }
    }

    [System.Serializable]
    struct ListLevelSave
    {
        public List<LevelData> listLevelSave;
    }
}
