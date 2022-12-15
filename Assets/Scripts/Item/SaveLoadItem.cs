using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> listGem = new List<string>();
        List<string> listCherry = new List<string>();

        GameSession listGame = FindObjectOfType<GameSession>();
        foreach (var item in listGame.ListGemItem)
        {
            listGem.Add(item);
        }
        foreach (var item in listGame.ListCherryItem)
        {
            listCherry.Add(item);
        }

        LoadGem(listGem);
        LoadCherry(listCherry);
    }

    private void LoadGem(List<string> list)
    {
        GemPickUp[] listCurentItem = GetComponentsInChildren<GemPickUp>();
        for (int i = 0; i < listCurentItem.Length; i++)
        {
            if (list.Contains(listCurentItem[i].name.ToString()))
            {
                Destroy(listCurentItem[i].gameObject);
            }
        }
    }

    private void LoadCherry(List<string> list)
    {
        CherryPickup[] listCurentItem = GetComponentsInChildren<CherryPickup>();
        for (int i = 0; i < listCurentItem.Length; i++)
        {
            if (list.Contains(listCurentItem[i].name.ToString()))
            {
                Destroy(listCurentItem[i].gameObject);
            }
        }
    }
}
