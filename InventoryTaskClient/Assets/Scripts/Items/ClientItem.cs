using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script provides connection between Item and Unity interface to store specific data for creating items
/// </summary>
public abstract class ClientItem : MonoBehaviour
{
    public Sprite ItemSprite;
    public int Weight;

    public Vector3 LocalScale      { get; private set; }
    public Item Item               { get; set; } = new Item();

    protected bool ResetPositionToOriginal;
    protected bool AllowMouseInteraction;
    protected GameObject gameRoot;

    protected virtual void Start() { UpdateData(); }

    protected virtual void UpdateData()
    {
        gameRoot                = GameObject.Find("GameRoot");
        Item.Weight             = Weight;
        Item.Name               = gameObject.name;
        LocalScale              = transform.localScale;
        var draggableObject     = GetComponent<DraggableObject>();
        AllowMouseInteraction   = draggableObject.AllowMouseInteraction;
        ResetPositionToOriginal = draggableObject.ResetPositionToOriginal;
    }

    public abstract GameObject CreateNewItem();
}
