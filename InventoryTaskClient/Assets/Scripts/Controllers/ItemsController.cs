using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General controller to manage creation of new items
/// Ideally could be improved as factory for any world objects (e.g given by server)
/// </summary>
public class ItemsController : MonoBehaviour
{
    public static ItemsController Obj;
    
    private GameObject gameRoot;

    private void Awake() { Obj = this; }

    private void Start() { gameRoot = transform.parent.gameObject; }

    /// <summary> Fill new item object with needed components and data from some container </summary>
    public GameObject CreateItemObject(ClientItem itemData)
    {
        var newItemObject = new GameObject(itemData.Item.Name);
        newItemObject.transform.SetParent(gameRoot.transform);
        newItemObject.transform.localScale = itemData.LocalScale;
        var boxCollider2D  = newItemObject.AddComponent<BoxCollider2D>();
        boxCollider2D.size = itemData.BoxColliderSize;
        var clientItem        = newItemObject.AddComponent<ClientItem>();
        clientItem.Item       = itemData.Item;
        clientItem.Weight     = itemData.Weight;
        clientItem.ItemSprite = itemData.ItemSprite;
        newItemObject.AddComponent<DraggableObject>();
        var spriteRenderer    = newItemObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = clientItem.ItemSprite;
        return newItemObject;
    }
}
