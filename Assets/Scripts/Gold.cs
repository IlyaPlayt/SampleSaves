using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Metal
{
    private void Awake()
    {
        _cindOfMetal = Inventory.Metal.gold;
    }
    
}
