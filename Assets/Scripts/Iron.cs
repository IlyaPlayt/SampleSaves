using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : Metal
{
    private void Awake()
    {
        _cindOfMetal = Inventory.Metal.iron;
    }

   
}
