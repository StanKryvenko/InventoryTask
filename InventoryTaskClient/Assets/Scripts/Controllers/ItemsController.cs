using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public static ItemsController Obj;
    
    private GameObject gameRoot;

    private void Awake() { Obj = this; }

    private void Start()
    {
        gameRoot = transform.parent.gameObject;
    }

    public GameObject CreateItemObject(ClientItem itemData)
    {
        var newItemObject = new GameObject(itemData.Item.Name);
        newItemObject.transform.SetParent(gameRoot.transform);
        newItemObject.transform.localScale = itemData.LocalScale;
        var boxCollider2D  = newItemObject.AddComponent<BoxCollider2D>();
        boxCollider2D.size = itemData.BoxColliderSize;
        var clientItem                 = newItemObject.AddComponent<ClientItem>();
        clientItem.Item                = itemData.Item;
        clientItem.Weight              = itemData.Weight;
        clientItem.ItemSprite          = itemData.ItemSprite;
        newItemObject.AddComponent<DraggableObject>();
        var spriteRenderer = newItemObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = clientItem.ItemSprite;
        return newItemObject;
    }
}
