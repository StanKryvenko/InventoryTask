using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ClientInventory))]
public class Backpack : InteractableSprite
{
    public GameObject InventoryItemTemplate;
    public GameObject SlotTemplate;
    public GameObject BackpackUITemplate;
    
    private GameObject slotsContainer;
    private GameObject backpackUI;
    private ClientInventory inventory;
    private List<GameObject> slotObjects    = new List<GameObject>();
    private HashSet<InventoryItem> itemsList = new HashSet<InventoryItem>();

    private bool isBackpackOpened;
    
    private const float InventoryMagnetDistance = 100f;

    private void Start()
    {
        inventory      = GetComponent<ClientInventory>();
        backpackUI     = Instantiate(BackpackUITemplate, UIController.Obj.transform);
        slotsContainer = backpackUI.transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
        
        Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && Vector2.Distance(Input.mousePosition, transform.position) <= InventoryMagnetDistance)
            TryToPutItem();
    }

    private void TryToPutItem()
    {
        var carriedItem = InteractionManager.Obj.CarriedItem != null ? InteractionManager.Obj.CarriedItem : InteractionManager.Obj.LastCarriedItem;
        if (carriedItem)
        {
            // Put an item into inventory
            var slotIndex = inventory.Inventory.TryPutItem(carriedItem.Item);
            if (slotIndex == -1) return;
            if (slotObjects.Count > slotIndex && slotObjects[slotIndex].transform.childCount == 0)
            {
                var inventoryItem = Instantiate(InventoryItemTemplate, slotObjects[slotIndex].transform).GetComponent<InventoryItem>();
                if (inventoryItem != null)
                {
                    itemsList.Add(inventoryItem);
                    inventoryItem.SetInventoryItem(slotIndex, carriedItem.ItemSprite, carriedItem);
                }
            }
            
            inventory.PutItemEvent?.Invoke(carriedItem);
            Destroy(carriedItem.gameObject);
        }
    }

    private void TryToTakeItem()
    {
        var selectedItem = itemsList.FirstOrDefault(x => x.IsSelected);
        if (selectedItem != null)
        {
            var quantityLeft = inventory.Inventory.TryTakeItem(selectedItem.SlotID);
            if (quantityLeft == -1) return;
            if (quantityLeft == 0) Destroy(selectedItem.gameObject);
            var itemObject  = ItemsController.Obj.CreateItemObject(selectedItem.Item);
            var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z   = 0;
            itemObject.transform.position = newPosition;
            // Force act for taking object
            itemObject.GetComponent<DraggableObject>()?.OnActLeft(null);
            inventory.TakeItemEvent?.Invoke(selectedItem.Item);
        }
    }

    private void Initialize()
    {
        for (var i = 0; i < inventory.NumBackpackSlots; i++)
        {
            slotObjects.Add(Instantiate(SlotTemplate, slotsContainer.transform));
        }
    }
    
    public override InteractResult OnActRight(InteractionContext context)
    {
        isBackpackOpened = true;
        backpackUI.SetActive(true);
        return InteractResult.Success;
    }
    
    public override InteractResult OnActRightEnd(InteractionContext context)
    {
        if (isBackpackOpened)
        {
            isBackpackOpened = false;
            backpackUI.SetActive(false);
            TryToTakeItem();
            foreach (var item in itemsList) item.IsSelected = false;
            return InteractResult.Success;
        }
        return InteractResult.Fail;
    }
}
