using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller to give access to main UI
/// </summary>
public class UIController : MonoBehaviour
{
    public static UIController Obj;

    private void Awake()
    {
        Obj = this;
    }
}
