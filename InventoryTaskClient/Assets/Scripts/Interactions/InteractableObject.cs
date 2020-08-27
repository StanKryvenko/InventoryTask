using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Implements interaction for objects
/// </summary>
public abstract class InteractableObject : MonoBehaviour, IInteractable, IPointerDownHandler, IPointerUpHandler
{
    public bool AllowMouseInteraction = true;
    public virtual InteractResult OnActLeft()     => InteractResult.Success;
    public virtual InteractResult OnActRight()    => InteractResult.Success;
    public virtual InteractResult OnActLeftEnd()  => InteractResult.Success;
    public virtual InteractResult OnActRightEnd() => InteractResult.Success;

    private void OnMouseDown()
    {
        if (!AllowMouseInteraction) return;
        if (Input.GetMouseButtonDown(0))      OnActLeft();
        else if (Input.GetMouseButtonDown(1)) OnActRight();
    }

    private void OnMouseUp()
    {
        if (!AllowMouseInteraction) return;
        if (Input.GetMouseButtonUp(0))      OnActLeftEnd();
        else if (Input.GetMouseButtonUp(1)) OnActRightEnd();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnActLeft();
                break;
            case PointerEventData.InputButton.Right:
                OnActRight();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnActLeftEnd();
                break;
            case PointerEventData.InputButton.Right:
                OnActRightEnd();
                break;
        }
    }
}
