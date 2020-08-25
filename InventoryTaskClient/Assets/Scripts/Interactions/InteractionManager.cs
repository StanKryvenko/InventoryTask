using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General manager for storing interaction data
/// </summary>
public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Obj;
    
    public ClientItem LastCarriedItem { get; private set; }
    public ClientItem CarriedItem { get; private set; }

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
}
