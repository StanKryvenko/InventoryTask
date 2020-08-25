using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Item : Stackable, IInteractable, IController
{
    public int ID { get; set; }
    public int Weight { get; set; }
    public override string Name { get => this.Type.Name; set => throw new NotImplementedException(); }
    public virtual Type Type    => this.GetType();

    public virtual InteractResult OnActLeft(InteractionContext context)  => InteractResult.Success;
    public virtual InteractResult OnActRight(InteractionContext context) => InteractResult.Success;
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