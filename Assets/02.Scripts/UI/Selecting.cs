using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selecting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    private Image fileImage;
    public Color selectColor;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    RectTransform coolRect;
    RectTransform rectTransform;

    Vector3 defaultPos;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        fileImage = GetComponent<Image>();
    }

    #region OnSelecting
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color32 color = selectColor;
        color.a -= 40;
        fileImage.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fileImage.color = Color.clear;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fileImage.color = selectColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        coolRect.anchoredPosition = rectTransform.anchoredPosition;
    }

    #endregion
}
