using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#region Components
// Inventory components describe additional properties for an inventory
// It could be weight restrictions, Type and etc.
public abstract class InventoryComponent
{
    public abstract void Initialize(Inventory inventory);

    public abstract void ApplyChange(Item item, int quantityDelta);
    public abstract void EndChangeSetModification(bool successful);
}

// tracks the total weight of an inventory
public class WeightComponent : InventoryComponent
{
    public int MaxWeight { get; private set; }
    
    private int weight;
    private int weightDelta;

    private Inventory inventory;

    public int Weight => weight + weightDelta;

    public WeightComponent(int maxWeight)
    {
        MaxWeight = maxWeight;
    }

    public void SetMaxWeight(int maxWeight) => MaxWeight = maxWeight;

    public override void Initialize(Inventory inventory)
    {
        weight = inventory.Stacks.Sum(stack => (stack.Item?.Weight ?? 0) * stack.Quantity);
        this.inventory = inventory;
    }

    public override void ApplyChange(Item item, int quantityDelta)
    {
        weightDelta += item.Weight * quantityDelta;
    }

    public override void EndChangeSetModification(bool successful)
    {
        if (successful && this.weightDelta != 0) this.weight += this.weightDelta;
        this.weightDelta = 0;
    }
}
#endregion

public abstract class Inventory
{
    #region Components
    private List<InventoryComponent> components = new List<InventoryComponent>();
    public IEnumerable<InventoryComponent> Components => this.Parent == null ? this.components : this.components.Concat(this.Parent.Components);

    public void AddComponent(InventoryComponent component)
    {
        component.Initialize(this);
        this.components.Add(component);
    }
    #endregion
    
    private readonly List<ItemStack> stacks = new List<ItemStack>();
    public virtual IEnumerable<ItemStack> Stacks => this.stacks;

    public Inventory Parent { get; private set; }

    public Inventory(int numStacks)
    {
        this.AddStacks(numStacks);
    }
    
    public void AddStacks(int numStacks)
    {
        for (var i = 0; i < numStacks; i++) this.stacks.Add(new ItemStack());
    }

    public static bool MoveItems(ItemStack sourceStack, ItemStack targetStack, int quantityOverride = -1)
    {
        if (sourceStack.Item == null) return false;
        var quantity = quantityOverride > 0 && sourceStack.Quantity >= quantityOverride ? quantityOverride : sourceStack.Quantity;

        return true;
    }
    
    protected ItemStack GetStack(int index)           => index >= 0 && index < this.Size ? this.stacks[index] : null;
    protected int       IndexOfStack(ItemStack stack) => this.stacks.IndexOf(stack);
    protected int       Size                          => this.stacks.Count;
}
