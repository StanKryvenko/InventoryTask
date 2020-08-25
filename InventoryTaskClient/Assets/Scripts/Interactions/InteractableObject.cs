using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public virtual InteractResult OnActLeft(InteractionContext context)     => InteractResult.Success;
    public virtual InteractResult OnActRight(InteractionContext context)    => InteractResult.Success;
    public virtual InteractResult OnActLeftEnd(InteractionContext context)  => InteractResult.Success;
    public virtual InteractResult OnActRightEnd(InteractionContext context) => InteractResult.Success;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))      OnActLeft(InteractionManager.Obj.CurrentContext);
        else if (Input.GetMouseButtonDown(1)) OnActRight(InteractionManager.Obj.CurrentContext);
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))      OnActLeftEnd(InteractionManager.Obj.CurrentContext);
        else if (Input.GetMouseButtonUp(1)) OnActRightEnd(InteractionManager.Obj.CurrentContext);
    }
}
