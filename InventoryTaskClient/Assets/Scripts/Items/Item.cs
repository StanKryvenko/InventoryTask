using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which provides needed data for storing item (its weight, id and name)
/// Ideally ID should be filled from server
/// </summary>
public class Item : Stackable, IController
{
    public int ID { get; set; }
    public int Weight { get; set; }
    public override string Name { get; set; }
}

/// <summary>
/// Describes stacks data for inventory (item info and quantity)
/// </summary>
public class ItemStack
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public ItemStack()
    { }

    public ItemStack(Item item, int quantity)
    {
        this.Item = item;
        this.Quantity = quantity;
    }
}