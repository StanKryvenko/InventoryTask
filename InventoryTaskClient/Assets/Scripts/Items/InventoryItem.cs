using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class stores item data inside inventory after putting an item into it, the data could be used to recreating an item
/// </summary>
[RequireComponent(typeof(Sprite))]
public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ClientItem Item;
    public int SlotID;
    public bool IsSelected { get; set; }

    private Image image;
    private Sprite sprite;

    private void Awake()
    {
        image  = GetComponent<Image>();
        image.sprite = sprite;
    }
    
    public void SetInventoryItem(int slotId, Sprite sprite, ClientItem item)
    {
        this.SlotID = slotId;
        this.sprite = sprite;
        Item        = item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelected = false;
    }
}
