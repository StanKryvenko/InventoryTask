﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is an implementation of an inventory - backpack inventory
/// It contains UI controls, managing item icons between slots, setting quantities, sending events
/// Also it contains methods for activating itself
/// </summary>
[RequireComponent(typeof(ClientInventory))]
public class Backpack : InteractableObject
{
    public GameObject InventoryItemTemplate;
    public GameObject SlotTemplate;
    public GameObject BackpackUITemplate;
    
    private GameObject slotsContainer;
    private GameObject backpackUI;
    private ClientInventory inventory;
    private List<(GameObject slot, Text quantity)> slotObjects = new List<(GameObject, Text)>();
    private List<InventoryItem> itemsList = new List<InventoryItem>();

    private bool isBackpackOpened;
    
    public float InventoryMagnetDistance = 100f;   // Distance in which it will take an item if it's dropped

    private void Start()
    {
        inventory      = GetComponent<ClientInventory>();
        backpackUI     = Instantiate(BackpackUITemplate, UIController.Obj.transform);
        slotsContainer = backpackUI.transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
        
        Initialize();
    }

    private void Update()
    {
        DetectItemNearby();
    }

    /// <summary> Check an item around and if an object is dropped near to backpack sprite </summary>
    private void DetectItemNearby()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var carriedItem = InteractionManager.Obj.CarriedItem != null ? InteractionManager.Obj.CarriedItem : InteractionManager.Obj.LastCarriedItem;
            if (carriedItem)
            {
                if (InteractionManager.Obj.AllowRaycasting && InteractionManager.Obj.SelectedObject == gameObject)
                {
                    if (Vector3.Distance(carriedItem.transform.position, transform.position) <= InventoryMagnetDistance)
                        TryToPutItem(carriedItem);
                }
                else
                {
                    if (Vector2.Distance(Input.mousePosition, transform.position) <= InventoryMagnetDistance)
                        TryToPutItem(carriedItem);
                }
            }
        }
    }

    /// <summary> Tries to put an item to inventory, saves item data in inventory item inside slots, updates quantities </summary>
    private void TryToPutItem(ClientItem carriedItem)
    {
        // Put an item into inventory
        var slotIndex = inventory.Inventory.TryPutItem(carriedItem.Item);
        if (slotIndex == -1)
        {
            carriedItem.GetComponent<DraggableObject>().ResetPosition();
            return;
        }
        // Save item data in inventory item inside a slot
        if (slotObjects.Count > slotIndex && slotObjects[slotIndex].slot.transform.childCount == 0)
        {
            var inventoryItem = Instantiate(InventoryItemTemplate, slotObjects[slotIndex].slot.transform).GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                itemsList.Add(inventoryItem);
                inventoryItem.SetInventoryItem(slotIndex, carriedItem.ItemSprite, carriedItem);
            }
        }
        // Update quantity info
        var quantityLeft = inventory.Inventory.GetQuantityInSlot(slotIndex);
        if (quantityLeft != -1) slotObjects[slotIndex].quantity.text = quantityLeft.ToString();
        inventory.PutItemEvent?.Invoke(carriedItem);
        Destroy(carriedItem.gameObject);
    }

    /// <summary> Tries taking item from inventory with holding object with mouse and updating quantity info </summary>
    private void TryToTakeItem()
    {
        var selectedItem = itemsList.FirstOrDefault(x => x.IsSelected);
        if (selectedItem != null)
        {
            // Try to take an item and hold it with mouse
            var quantityLeft = inventory.Inventory.TryTakeItem(selectedItem.SlotID);
            if (quantityLeft == -1) return;
            if (quantityLeft == 0) Destroy(selectedItem.gameObject);
            var itemObject      = selectedItem.Item.CreateNewItem();
            
            slotObjects[selectedItem.SlotID].quantity.text = quantityLeft.ToString();
            // Force act for taking object
            var draggableObject = itemObject.GetComponent<DraggableObject>();
            if (draggableObject != null) draggableObject.OnActLeft();
            inventory.TakeItemEvent?.Invoke(selectedItem.Item);
        }
    }

    /// <summary> Inits slots objects with quantity text components </summary>
    private void Initialize()
    {
        for (var i = 0; i < inventory.NumBackpackSlots; i++)
        {
            var newSlotObject    = Instantiate(SlotTemplate, slotsContainer.transform);
            var slotQuantityPair = (newSlotObject.GetComponentInChildren<GridLayoutGroup>().gameObject, newSlotObject.GetComponentInChildren<Text>());
            slotObjects.Add(slotQuantityPair);
        }
    }
    
    public override InteractResult OnActRight()
    {
        // Show backpack UI
        isBackpackOpened = true;
        backpackUI.SetActive(true);
        InteractionManager.Obj.IsLockedControls = true;
        return InteractResult.Success;
    }
    
    public override InteractResult OnActRightEnd()
    {
        // Hide opened backpack UI and try to take selected item
        if (isBackpackOpened)
        {
            isBackpackOpened = false;
            backpackUI.SetActive(false);
            TryToTakeItem();
            foreach (var item in itemsList) item.IsSelected = false;
            InteractionManager.Obj.IsLockedControls = false;
            return InteractResult.Success;
        }
        return InteractResult.Fail;
    }
}
