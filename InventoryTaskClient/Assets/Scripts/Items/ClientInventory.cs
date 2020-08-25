using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClientItemEvent : UnityEvent<ClientItem> {};

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
    }
}

