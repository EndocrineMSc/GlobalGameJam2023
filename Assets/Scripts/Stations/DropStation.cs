using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStation : Station
{
    protected override void OnPlayerUse()
    {
        Debug.Log("Dropping");
    }
}
