using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selecting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image fileImage;
    public Color selectColor;

    private void Start()
    {
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
    #endregion
}
