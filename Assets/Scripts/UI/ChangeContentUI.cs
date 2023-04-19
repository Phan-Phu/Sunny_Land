using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeContentUI : MonoBehaviour
{
    [SerializeField] private GameObject firstContent;
    [SerializeField] private GameObject secondContent;
    [SerializeField] private Button skip;
    private bool checkShow = true;

    public void ChangeContent()
    {
        firstContent.SetActive(!checkShow);
        secondContent.SetActive(checkShow);
        skip.gameObject.SetActive(false);
    }
}