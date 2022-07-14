using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selecting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler
{
    private Image fileImage;
    public Color selectColor;
    [SerializeField]
    RectTransform coolRect;
    RectTransform rectTransform;

    Camera _cam;
    Vector3 offset;

    private void Start()
    {
        _cam = Camera.main;
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
        Vector3 pos = transform.position;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            pos = hitInfo.point;
            pos.y = 0f;

            pos.x -= offset.x;
            pos.z -= offset.z;

            if (Mathf.Abs(pos.x) > 7f)
            {
                pos.x = transform.position.x;
            }
            if (Mathf.Abs(pos.z) > 3.3f)
            {
                pos.z = transform.position.z;
            }
        }
        rectTransform.transform.position = pos;
        coolRect.anchoredPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            offset = hitInfo.point - transform.position;
        }
    }

    #endregion
}
