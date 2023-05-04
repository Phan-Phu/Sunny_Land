using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeContentUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject firstContent;
    [SerializeField] private GameObject secondContent;
    [SerializeField] private GameObject animationText;
    private bool checkShow = true;

    public void ChangeContent()
    {
        firstContent.SetActive(!checkShow);
        secondContent.SetActive(checkShow);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeContent();
    }

    private void Start()
    {
        ShowAnimationText();
    }

    private void ShowAnimationText()
    {
        float waitTime = .5f;
        StartCoroutine(ShowAnimationText(waitTime));
    }

    private IEnumerator ShowAnimationText(float waitTime)
    {
        while(true)
        {
            animationText.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            animationText.SetActive(false);
            yield return new WaitForSeconds(waitTime);
        }
    }
}