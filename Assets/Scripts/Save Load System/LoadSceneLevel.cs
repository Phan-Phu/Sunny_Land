using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadSceneLevel : MonoBehaviour
{
    public event EventHandler<string> onLoadLevel;
    public event EventHandler<bool> onLockLevel;
    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        bool isOpen = ShowLevel.Instance.GetIsOpenLevel(gameObject.name);
        button.onClick.AddListener(LoadLevelScene);
        onLockLevel?.Invoke(this, isOpen);
    }

    private void LoadLevelScene()
    {
        string nameScene = gameObject.name;
        onLoadLevel?.Invoke(this, nameScene);
    }
}
