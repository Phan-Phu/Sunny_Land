using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnimationButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform rectTransformParent;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = rectTransformParent.gameObject.name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 30);
    }
}
