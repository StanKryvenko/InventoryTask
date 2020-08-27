using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientItem2D : ClientItem
{
    public Vector3 BoxColliderSize { get; private set; }
    
    protected override void UpdateData()
    {
        base.UpdateData();
        BoxColliderSize = GetComponent<BoxCollider2D>().size;
    }

    public override GameObject CreateNewItem()
    {
        var newItemObject = new GameObject(Item.Name);
        newItemObject.transform.SetParent(gameRoot.transform);
        newItemObject.transform.localScale = LocalScale;
        var boxCollider2D     = newItemObject.AddComponent<BoxCollider2D>();
        boxCollider2D.size    = BoxColliderSize;
        var clientItem        = newItemObject.AddComponent<ClientItem2D>();
        clientItem.Item       = Item;
        clientItem.Weight     = Weight;
        clientItem.ItemSprite = ItemSprite;
        var draggableObject   = newItemObject.AddComponent<DraggableObject>();
        draggableObject.ResetPositionToOriginal = ResetPositionToOriginal;
        draggableObject.AllowMouseInteraction   = AllowMouseInteraction;
        var spriteRenderer    = newItemObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = ItemSprite;
        
        clientItem.UpdateData();
        return newItemObject;
    }
}