using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Stackable, IController
{
    public int ID { get; set; }
    public int Weight { get; set; }
    public override string Name { get; set; }
}

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