using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingStation : Station
{
    protected override void OnPlayerUse()
    {
        Debug.Log("Digging");
    }
}