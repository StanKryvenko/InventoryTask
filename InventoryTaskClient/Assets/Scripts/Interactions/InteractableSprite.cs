using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Implements interaction for sprites (UI)
/// </summary>
public abstract class InteractableSprite : MonoBehaviour, IInteractable, IPointerDownHandler, IPointerUpHandler
{
    public virtual InteractResult OnActLeft()     => InteractResult.Success;
    public virtual InteractResult OnActRight()    => InteractResult.Success;
    public virtual InteractResult OnActLeftEnd()  => InteractResult.Success;
    public virtual InteractResult OnActRightEnd() => InteractResult.Success;

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