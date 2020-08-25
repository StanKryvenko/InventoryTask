using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script provides connection between Item and Unity interface to store specific data for creating items
/// </summary>
public class ClientItem : MonoBehaviour
{
    public Sprite ItemSprite;
    public int Weight;

    public Item Item               { get; set; } = new Item();
    public Vector3 LocalScale      { get; private set; }
    public Vector3 BoxColliderSize { get; private set; }
    
    private protected void Start()
    {
        Item.Weight     = Weight;
        LocalScale      = transform.localScale;
        BoxColliderSize = GetComponent<BoxCollider2D>().size;
        Item.Name       = gameObject.name;
    }
}