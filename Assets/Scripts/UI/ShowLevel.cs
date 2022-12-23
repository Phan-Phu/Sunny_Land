using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ShowLevel : MonoBehaviour
{
    public static ShowLevel Instance;

    [SerializeField] private List<LoadSceneLevel> loadSceneLevelList = new List<LoadSceneLevel>();
    [SerializeField] private GameObject lockLevelPrefab;

    private List<string> nameSceneLevelList = new List<string>();
    private List<LevelData> levelDataList = new List<LevelData>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Has more than Show Level " + Instance + ", " + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        List<LevelData> levelDataList = SaveLoadSystem.Instance.GetSaveSystem().LoadDataLevel();

        if (levelDataList == null)
        {
            InitializeListLevel();
            string nameLevel = "Level 1";
            UpdateLevel(nameLevel);
        }
        else
        {
            print("Open data list level");
            this.levelDataList = levelDataList;
        }

        foreach (LoadSceneLevel item in loadSceneLevelList)
        {
            item.onLockLevel += (object sender, bool isOpen) =>
            {
                if (isOpen == false)
                {
                    LoadSceneLevel loadSceneLevel = sender as LoadSceneLevel;
                    LockLevel(loadSceneLevel);
                }
            };
        }
    }

    private void InitializeListLevel()
    {
        print("Initialize list level");
        LoadSceneLevel[] loadSceneLevelArray = GetComponentsInChildren<LoadSceneLevel>();

#if UNITY_EDITOR
        // get name scene in build settings
        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
            name = name.Substring(0, name.Length - 6);
            string subname = name.Substring(0, 5);
            if (subname == "Level")
            {
                nameSceneLevelList.Add(name);
            }
        }
#else
        foreach (LoadSceneLevel item in loadSceneLevelArray)
        {
            string name = item.gameObject.name;
            nameSceneLevelList.Add(name);
            Debug.Log(name);
        }
#endif

        foreach (string nameSceneLevel in nameSceneLevelList)
        {
            LevelData levelData = new LevelData(nameSceneLevel, false);
            levelDataList.Add(levelData);
        }

        ISaveSystem saveSystem = SaveLoadSystem.Instance.GetSaveSystem();
        saveSystem.SaveDataLevel(levelDataList);
    }

    private void LockLevel(LoadSceneLevel loadSceneLevel)
    {
        GameObject parentRectGameObject = loadSceneLevel.gameObject;
        string nameScene = parentRectGameObject.name;
        print(nameScene + " is locked");
        Instantiate(lockLevelPrefab, parentRectGameObject.transform);
    }

    private void UpdateLevel(string sceneName)
    {
        print("Update Level Map");
        List<LevelData> levelDataList = new List<LevelData>();
        levelDataList = SaveLoadSystem.Instance.GetSaveSystem().LoadDataLevel();
        SaveSystem.Instance.DeleteDataLevel();

        foreach (LevelData levelData in levelDataList)
        {
            if (levelData.nameLevel == sceneName)
            {
                levelData.isOpen = true;
            }
        }

        ISaveSystem saveSystem = SaveLoadSystem.Instance.GetSaveSystem();
        saveSystem.SaveDataLevel(levelDataList);
    }

    public bool GetIsOpenLevel(string nameLevel)
    {
        List<LevelData> levelDataList = SaveLoadSystem.Instance.GetSaveSystem().LoadDataLevel();
        foreach (LevelData levelData in levelDataList)
        {
            if (levelData.nameLevel == nameLevel)
            {
                return levelData.isOpen;
            }
        }
        return false;
    }
}