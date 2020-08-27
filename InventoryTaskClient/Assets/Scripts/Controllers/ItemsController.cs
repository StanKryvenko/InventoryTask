using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General controller to manage creation of new items
/// Ideally could be improved as factory for any world objects (e.g given by server)
/// </summary>
public class ItemsController : MonoBehaviour
{
    public static ItemsController Obj;
    
    private void Awake() { Obj = this; }


}
