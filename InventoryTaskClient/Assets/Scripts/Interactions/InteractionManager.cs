using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General manager for storing interaction data
/// </summary>
public class InteractionManager : MonoBehaviour
{
    public float InteractionDistance = 10f;
    public static InteractionManager Obj;

    public bool AllowRaycasting   = true;
    public bool AllowControlsLock = true;
    public GameObject HandObject;

    private bool isLockedControls = false;

    public bool IsLockedControls
    {
        get => isLockedControls;
        set
        {
            if (!AllowControlsLock) return;
            isLockedControls = value;
            Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private GameObject selectedObject;
    public GameObject SelectedObject
    {
        get => selectedObject;
        set
        {
            selectedObject = value;
            selectedInteractable = value != null ? value.GetComponent<IInteractable>() : null;
        } 
    }
    public ClientItem LastCarriedItem { get; private set; }
    public ClientItem CarriedItem { get; private set; }

    private IInteractable selectedInteractable;
    
    private void Awake() { Obj = this; }

    public bool TrySelectingItem(ClientItem item)
    {
        if (CarriedItem != null) return false;

        CarriedItem = item;
        return true;
    }

    public void RemoveSelectedItem()
    {
        if (CarriedItem != null)
        {
            LastCarriedItem = CarriedItem;
            CarriedItem     = null;
        }
    }

    private void Update()
    {
        if (SelectedObject != null)
        {
            if (Input.GetMouseButtonDown(0))      selectedInteractable.OnActLeft();
            else if (Input.GetMouseButtonDown(1)) selectedInteractable.OnActRight();
            else if (Input.GetMouseButtonUp(0))   selectedInteractable.OnActLeftEnd();
            else if (Input.GetMouseButtonUp(1))   selectedInteractable.OnActRightEnd();
        }
    }
}
