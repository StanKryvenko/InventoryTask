using UnityEngine;

/// <summary>
/// Put this script onto item object to provide drag'n'drop feature
/// </summary>
public class DraggableObject : InteractableObject
{
    private Vector3 screenPoint = Vector3.zero;
    private Vector3 offset;

    private Vector3 originalPosition;

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

    public override InteractResult OnActLeft()
    {
        if (!IsDragging && Camera.main != null)
        {
            offset           = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            IsDragging       = true;
            originalPosition = transform.position;
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
    
    public override InteractResult OnActLeftEnd()
    {
        if (IsDragging) IsDragging = false;
        return InteractResult.Success;
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
    }
}
