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

    public abstract bool TryApplyChange(Item item, int quantityDelta);

    public abstract void ApplyDeltas();
}

// tracks the total weight of an inventory
public class WeightComponent : InventoryComponent
{
    public int MaxWeight { get; private set; }
    
    public int Weight;
    private int weightDelta;

    private Inventory inventory;

    public WeightComponent(int maxWeight)
    {
        MaxWeight = maxWeight;
    }

    public override void Initialize(Inventory inventory)
    {
        Weight = inventory.Stacks.Sum(stack => (stack.Item?.Weight ?? 0) * stack.Quantity);
        this.inventory = inventory;
    }

    public override bool TryApplyChange(Item item, int quantityDelta)
    {
        weightDelta = item.Weight * quantityDelta;
        if (Weight + weightDelta <= MaxWeight) return true;
        Debug.Log("Inventory is too heavy. Take off some heavy items to put more");
        return false;

    }

    public override void ApplyDeltas()
    {
        Weight += weightDelta;
    }
}
#endregion

public class Inventory
{
    public const int MaxItemsInStack = 10;
    
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

    public int TryPutItem(Item item)
    {
        // At first, let's find a stack with same item type and it's not full
        var stack = this.Stacks.FirstOrDefault(x => x.Item != null && x.Item.Name == item.Name && x.Quantity < MaxItemsInStack);
        if (stack == null)
        {
            // If there's no way to stack, find an empty space
            stack = this.Stacks.FirstOrDefault(x => x.Item == null);
            if (stack == null) return -1;
        }

        // Check components restrictions and apply temp value if it's ok
        bool applyResult;
        foreach (var component in components)
        {
            applyResult = component.TryApplyChange(item, 1);
            if (!applyResult) return -1;
        }

        // Add new item to stack
        if (stack.Item == null) stack.Item = item;
        stack.Quantity++;
        
        // Apply component temp changes
        foreach (var component in components) component.ApplyDeltas();
        return IndexOfStack(stack);
    }

    public int GetQuantityInSlot(int slotId)
    {
        var stack = GetStack(slotId);
        if (stack != null) return stack.Quantity;
        return -1;
    }

    /// <summary> Try to take item from stack (if it exists) </summary>
    public int TryTakeItem(int stackIndex)
    {
        var stack = GetStack(stackIndex);
        if (stack?.Quantity > 0)
        {
            stack.Quantity--;
            foreach (var component in components)
            {
                component.TryApplyChange(stack.Item, -1);
                component.ApplyDeltas();
            }
            if (stack.Quantity == 0) stack.Item = null;
            return stack.Quantity;
        }

        return -1;
    }
    
    protected ItemStack GetStack(int index)           => index >= 0 && index < this.Size ? this.stacks[index] : null;
    protected int       IndexOfStack(ItemStack stack) => this.stacks.IndexOf(stack);
    protected int       Size                          => this.stacks.Count;
}