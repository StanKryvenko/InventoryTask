using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private bool isDragging;
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        isDragging         = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        transform.localPosition = Vector3.zero;
        isDragging              = false;
    }
}