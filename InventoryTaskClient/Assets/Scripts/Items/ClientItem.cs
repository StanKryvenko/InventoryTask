using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientItem : MonoBehaviour
{
    public Vector3 LocalScale      { get; private set; }
    public Vector3 BoxColliderSize { get; private set; }
    public Sprite ItemSprite;
    public Item Item { get; set; } = new Item();
    public int Weight;

    private protected void Start()
    {
        Item.Weight     = Weight;
        LocalScale      = transform.localScale;
        BoxColliderSize = GetComponent<BoxCollider2D>().size;
        Item.Name       = gameObject.name;
    }
}