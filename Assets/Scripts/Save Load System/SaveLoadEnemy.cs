using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadEnemy : MonoBehaviour
{
    private void Start()
    {
        List<string> listEnemy = new List<string>();
        GameSession gameSession = GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>();
        foreach (var item in gameSession.ListEnemy)
        {
            listEnemy.Add(item);
        }

        LoadEnemy(listEnemy);
    }

    private void LoadEnemy(List<string> listEnemy)
    {
        Enemy[] enemyArray = GetComponentsInChildren<Enemy>();
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (listEnemy.Contains(enemyArray[i].name.ToString()))
            {
                Destroy(enemyArray[i].gameObject);
            }
        }
    }
}
