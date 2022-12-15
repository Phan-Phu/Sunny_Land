using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; // convert file to binary
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SaveDataLevel()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levels.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LevelData data = new LevelData(currentScene);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }
    public static LevelData LoadDataLevel()
    {
        string path = Application.persistentDataPath + "/levels.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file is not found in: " + path);
            return null;
        }
    }
    public static void DeleteDataLevel()
    {
        string path = Application.persistentDataPath + "/levels.dat";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning("Save file is not found in: " + path);
        }
    }
}
