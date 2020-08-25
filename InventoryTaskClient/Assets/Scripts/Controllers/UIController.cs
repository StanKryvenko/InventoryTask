using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Obj;

    private void Awake()
    {
        Obj = this;
    }
}
