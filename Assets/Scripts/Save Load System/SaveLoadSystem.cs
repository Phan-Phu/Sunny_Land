using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Has more than Save Load Scene " + Instance + ", " + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private ISaveSystem saveSystem;

    public ISaveSystem GetSaveSystem()
    {
        return saveSystem;
    }

    public void SetSaveSystem(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
    }
}
