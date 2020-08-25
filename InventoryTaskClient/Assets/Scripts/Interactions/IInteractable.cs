/// <summary>
/// An object that can be interacted with by the player.
/// </summary>
public interface IInteractable
{
    /// <summary> An interaction from the left mouse button. eg Picking an item. </summary>
    InteractResult OnActLeft(InteractionContext context);

    /// <summary> An interaction from the right mouse button. eg Placing an item. </summary>
    InteractResult OnActRight(InteractionContext context);
    /// <summary> An interaction from the left mouse button finished. </summary>
    InteractResult OnActLeftEnd(InteractionContext context);
    /// <summary> An interaction from the right mouse button finished. </summary>
    InteractResult OnActRightEnd(InteractionContext context);
}