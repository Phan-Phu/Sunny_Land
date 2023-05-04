using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISuccess : MonoBehaviour
{
    [SerializeField] GameObject produce;
    [SerializeField] GameObject success;
    [SerializeField] float timeDelay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        produce.SetActive(true);
        success.SetActive(false);
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(timeDelay);
        produce.SetActive(false);
        success.SetActive(true);
    }
}
