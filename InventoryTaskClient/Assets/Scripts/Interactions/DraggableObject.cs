using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class DraggableObject : InteractableObject
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private bool isDragging;
    protected bool IsDragging
    {
        get => isDragging;
        set
        {
            isDragging = value;
            var clientItem = GetComponent<ClientItem>();
            if (clientItem != null)
            {
                if (value) InteractionManager.Obj.TrySelectingItem(clientItem);
                else       InteractionManager.Obj.RemoveSelectedItem();
            }
        }
    }

    protected Vector3 OriginalPosition;

    public override InteractResult OnActLeft(InteractionContext context)
    {
        if (!IsDragging && Camera.main != null)
        {
            offset           = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            IsDragging       = true;
            OriginalPosition = transform.position;
            var clientItem   = GetComponent<ClientItem>();
            if (clientItem != null) InteractionManager.Obj.TrySelectingItem(clientItem);
        }
        return InteractResult.Success;
    }

    protected void Update()
    {
        if (IsDragging)
        {
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            var curPosition    = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    
    public override InteractResult OnActLeftEnd(InteractionContext context)
    {
        if (IsDragging) IsDragging = false;
        return InteractResult.Success;
    }
}
