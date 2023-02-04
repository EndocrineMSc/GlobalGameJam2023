using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStation : Station
{
    protected override void OnPlayerUse()
    {
        Debug.Log("Healing");
    }
}