using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextTitle : MonoBehaviour
{
    private Image frameTitle;
    private TextMeshProUGUI textTitle;
    [SerializeField] Sprite imageStart;
    [SerializeField] Sprite imageComplete;
    [SerializeField] float timeToDelay = 3f;

    private void Awake()
    {
        frameTitle = GameObject.FindGameObjectWithTag("Title").GetComponent<Image>();
        textTitle = frameTitle.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        string nameCurrentScene = SceneManager.GetActiveScene().name;
        frameTitle.sprite = imageStart;
        frameTitle.SetNativeSize();
        ShowTitle(nameCurrentScene);
    }

    public void ShowTitle(string name)
    {
        StartCoroutine(ShowText(name));
    }

    IEnumerator ShowText(string name)
    {
        frameTitle.enabled = true;
        textTitle.text = name;
        yield return new WaitForSeconds(timeToDelay);
        textTitle.text = "";
        frameTitle.enabled = false;
    }

    public void SetImage()
    {
        frameTitle.sprite = imageComplete;
        frameTitle.SetNativeSize();
    }
}
