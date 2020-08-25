using UnityEngine;

public class BackpackInventory : Inventory
{
    public BackpackInventory(int numStacks) : base(numStacks)
    {
        AddStacks(numStacks);
    }
}

public class Backpack : MonoBehaviour
{
    public int WeightLimit             = 30000;
    private const int NumBackpackSlots = 5;

    public BackpackInventory BackpackInventory = new BackpackInventory(NumBackpackSlots);

    private void Start()
    {
        BackpackInventory.AddComponent(new WeightComponent(WeightLimit));
    }
}
