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
        float holdDownButtonY = 5f;
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, holdDownButtonY);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float releaseButtonY = 30f;
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, releaseButtonY);
    }
}
