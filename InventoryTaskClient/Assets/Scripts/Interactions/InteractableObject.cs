using UnityEngine;

/// <summary>
/// Implements interaction for sprite (2d) objects
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public virtual InteractResult OnActLeft()     => InteractResult.Success;
    public virtual InteractResult OnActRight()    => InteractResult.Success;
    public virtual InteractResult OnActLeftEnd()  => InteractResult.Success;
    public virtual InteractResult OnActRightEnd() => InteractResult.Success;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))      OnActLeft();
        else if (Input.GetMouseButtonDown(1)) OnActRight();
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))      OnActLeftEnd();
        else if (Input.GetMouseButtonUp(1)) OnActRightEnd();
    }
}
