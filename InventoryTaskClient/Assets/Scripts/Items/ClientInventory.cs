using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClientItemEvent : UnityEvent<ClientItem> {};

/// <summary>
/// Gives access to inventory data from Unity, provides needed events and inventory initializations
/// </summary>
public class ClientInventory : MonoBehaviour
{
    public ClientItemEvent TakeItemEvent;
    public ClientItemEvent PutItemEvent;
    public Inventory Inventory { get; set; } = new Inventory();
    
    public int WeightLimit      = 30000;
    public int NumBackpackSlots = 5;

    private void Awake()
    {
        if (PutItemEvent == null)  PutItemEvent  = new ClientItemEvent();
        if (TakeItemEvent == null) TakeItemEvent = new ClientItemEvent();
        Inventory.AddStacks(NumBackpackSlots);
        Inventory.AddComponent(new WeightComponent(WeightLimit));
        
        PutItemEvent.AddListener(OnPutItem);
        PutItemEvent.AddListener(OnTakeItem);
    }

    // RPC System (remote procedure call) using for server - makes simpler life for developer
    // Using RPC System you will get a nice solution for fast calls on server-client architecture
    // Server could be made as .Net Core application with NoSQL database (like LiteDB) or other
    // This game includes ENet lib, which could be easily added to .Net Core application
    private void OnTakeItem(ClientItem item)
    {
        // Server.RPC("InventoryManager", "OnTakeItem", item.Item.ID);
    }

    private void OnPutItem(ClientItem item)
    {
        // Server.RPC("InventoryManager", "OnPutItem", item.Item.ID);
    }
    
}

