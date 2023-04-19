using UnityEngine;

[CreateAssetMenu(fileName = "story", menuName = "ScriptableObjects/Story/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}