using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableSprite : MonoBehaviour, IInteractable, IPointerDownHandler, IPointerUpHandler
{
    public virtual InteractResult OnActLeft(InteractionContext context)     => InteractResult.Success;
    public virtual InteractResult OnActRight(InteractionContext context)    => InteractResult.Success;
    public virtual InteractResult OnActLeftEnd(InteractionContext context)  => InteractResult.Success;
    public virtual InteractResult OnActRightEnd(InteractionContext context) => InteractResult.Success;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnActLeft(InteractionManager.Obj.CurrentContext);
                break;
            case PointerEventData.InputButton.Right:
                OnActRight(InteractionManager.Obj.CurrentContext);
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnActLeftEnd(InteractionManager.Obj.CurrentContext);
                break;
            case PointerEventData.InputButton.Right:
                OnActRightEnd(InteractionManager.Obj.CurrentContext);
                break;
        }
    }
}