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
        onLockLevel?.Invoke(this, isOpen);

        button.onClick.AddListener(LoadLevelScene);
    }

    private void LoadLevelScene()
    {
        string nameScene = gameObject.name;
        onLoadLevel?.Invoke(this, nameScene);
    }
}
