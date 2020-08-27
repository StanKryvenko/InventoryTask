using UnityEngine;

/// <summary>
/// Put this script onto item object to provide drag'n'drop feature
/// </summary>
public class DraggableObject : InteractableObject
{
    public bool ResetPositionToOriginal = true;
    private Vector3 screenPoint = Vector3.zero;
    private Vector3 offset;

    private Vector3 originalPosition;
    private int lastLayer;

    private const string IgnoreLayerName = "Ignore Raycast";

    private bool isDragging;
    protected bool IsDragging
    {
        get => isDragging;
        set
        {
            isDragging = value;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = value;
            var clientItem = GetComponent<ClientItem>();
            if (clientItem != null)
            {
                if (value) InteractionManager.Obj.TrySelectingItem(clientItem);
                else       InteractionManager.Obj.RemoveSelectedItem();
            }
            if (InteractionManager.Obj.HandObject != null)
            {
                if (value)
                {
                    transform.SetParent(InteractionManager.Obj.HandObject.transform);
                    transform.localPosition = Vector3.zero;
                    lastLayer               = gameObject.layer;
                    gameObject.layer        = LayerMask.NameToLayer(IgnoreLayerName);
                }
                else
                {
                    gameObject.layer = lastLayer;
                    transform.SetParent(null);
                }
            }
            else if (value)
            {
                var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPosition.z   = 0;
                transform.position = newPosition;
            }
        }
    }

    public override InteractResult OnActLeft()
    {
        if (!IsDragging && Camera.main != null)
        {
            IsDragging       = true;
            offset           = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            originalPosition = transform.position;
        }
        return InteractResult.Success;
    }

    protected void Update()
    {
        if (IsDragging)
        {
            if (InteractionManager.Obj.HandObject == null)
            {
                var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                var targetPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = targetPosition;
            }
            if (Input.GetMouseButtonUp(0)) OnActLeftEnd();
        }
    }
    
    public override InteractResult OnActLeftEnd()
    {
        if (IsDragging) IsDragging = false;
        return InteractResult.Success;
    }

    public void ResetPosition()
    {
        if (ResetPositionToOriginal) transform.position = originalPosition;
        else OnActLeftEnd();
    }
}
