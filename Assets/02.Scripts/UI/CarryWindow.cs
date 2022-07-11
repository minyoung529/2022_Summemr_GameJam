using UnityEngine;
using UnityEngine.EventSystems;

public class CarryWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform carriedObject;
    private RectTransform rectTransform;
    private Vector3 offset;

    private void Start()
    {
        rectTransform = carriedObject.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouse = Input.mousePosition - offset;
        rectTransform.anchoredPosition = mouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector3.zero;
    }
}